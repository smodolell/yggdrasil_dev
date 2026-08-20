using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using Yggdrasil.Common.Attributes;
using Yggdrasil.Common.Constants;
using Yggdrasil.Module.Credito.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.Features.Financial.Factories;
using Yggdrasil.Module.Credito.Features.Financial.Services;
using Yggdrasil.Module.Credito.Features.Operaciones.CapturaCredito.DTOs;

namespace Yggdrasil.Module.Credito.Features.Operaciones.CapturaCredito.Commands;

[Auditable(AuditEvents.EditarCredito)]
public record UpdateCreditoCommand(int Id, CreditoEditDto Model) : ICommand<Result>;

internal class UpdateCreditoCommandHandler(
    IApplicationDbContext context,
    IUnitOfWork unitOfWork,
    IValidator<CreditoEditDto> validator,
    IAmortizationService amortizationService
) : ICommandHandler<UpdateCreditoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<CreditoEditDto> _validator = validator;
    private readonly IAmortizationService _amortizationService = amortizationService;

    public async Task<Result> HandleAsync(UpdateCreditoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;

        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        var oCredito = await _context.FI_Credito
            .Include(c => c.FI_TablaAmortiza)
            .SingleOrDefaultAsync(c => c.Id == message.Id, cancellationToken);
        if (oCredito == null) return Result.NotFound($"[NO_EXISTE][{nameof(FI_Credito)}]");

        if (oCredito.EstatusCreditoId != AppConstants.CAT_EstatusCreditoId_CAPTURADO)
            return Result.Invalid(new ValidationError("Solo se pueden editar créditos en estatus Capturado"));

        var oPeriodicidad = await _context.CAT_Periodicidad.SingleOrDefaultAsync(r => r.Id == model.PeriodicidadId, cancellationToken);
        if (oPeriodicidad == null)
            return Result.Invalid(new ValidationError("Periodicidad no existe"));

        var oProducto = await _context.FI_Producto.SingleOrDefaultAsync(r => r.Id == oCredito.ProductoId, cancellationToken);
        if (oProducto == null)
            return Result.Invalid(new ValidationError("Se requiere el Producto"));

        var oMovimientoRenta = await _context.FI_TipoMovimiento.SingleOrDefaultAsync(r => r.Id == oProducto.TipoMovimientoRentaId, cancellationToken);
        if (oMovimientoRenta == null)
            return Result.Invalid(new ValidationError("Configure el tipo de movimiento Renta"));

        // Recalcula la tabla de amortización con los nuevos valores del crédito
        var calculate = new AmortizationDto
        {
            SaldoInicial = model.CapitalFinanciado,
            Plazo = model.Plazo!.Value,
            UsaDias = oPeriodicidad.UsaDias,
            ParamMes = oPeriodicidad.ParamMes,
            ParamDias = oPeriodicidad.ParamDias,
            FecInicioContrato = model.FechaInicio!.Value,
            FecPrimeraRenta = model.FechaPrimeraRenta!.Value,
            TasaAnual = Convert.ToDouble(model.Tasa / 100.0m),
            TasaIVA = (double)(model.TasaIva ?? 0) / 100.0
        };

        var calculateResult = _amortizationService.Calculate(calculate, (AmortizationMethod)model.TipoTablaAmortizaId);
        if (!calculateResult.IsSuccess)
        {
            return Result.Invalid(calculateResult.ValidationErrors);
        }

        var tablaAmortiza = calculateResult.Value;

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            oCredito.MonedaId = model.MonedaId!.Value;
            oCredito.PeriodicidadId = oPeriodicidad.Id;
            oCredito.Capital = model.Capital;
            oCredito.CapitalFinanciado = model.CapitalFinanciado;
            oCredito.Plazo = model.Plazo!.Value;
            oCredito.Tasa = model.Tasa;
            oCredito.PuntosMas = model.PuntosMas;
            oCredito.PuntosPor = model.PuntosPor;
            oCredito.TasaBase = (model.Tasa + model.PuntosMas) * model.PuntosPor;
            oCredito.TasaMora = model.TasaMora;
            oCredito.PuntosMasMora = model.PuntosMasMora;
            oCredito.PuntosPorMora = model.PuntosPorMora;
            oCredito.TasaBaseMora = (model.TasaMora + model.PuntosMasMora) * model.PuntosPorMora;
            oCredito.TasaIva = model.TasaIva ?? 0;
            oCredito.FechaAlta = model.FechaAlta ?? oCredito.FechaAlta;
            oCredito.FechaInicio = model.FechaInicio;
            oCredito.FechaPrimeraRenta = model.FechaPrimeraRenta;
            oCredito.VersionTabla += 1;


            _context.FI_TablaAmortiza.RemoveRange(oCredito.FI_TablaAmortiza);

            oCredito.FI_TablaAmortiza = tablaAmortiza.TablaAmortiza.Select(s => new FI_TablaAmortiza
            {
                Id = Guid.NewGuid(),
                FI_Credito = oCredito,
                TipoMovimientoId = oMovimientoRenta.Id,
                NoPago = s.NoPago,
                FechaInicial = s.FecInicio,
                FechaFinal = s.FecFinal,
                FechaVencimiento = s.FecVencimiento,
                SaldoInicial = s.SaldoInicial,
                Capital = s.Capital,
                Interes = s.Interes,
                Iva = s.IVA,
                Total = s.Total,
                SaldoFinal = s.SaldoFinal,
                Dias = s.Dias,
                VersionTabla = oCredito.VersionTabla,
                TasaCalculo = oCredito.Tasa,
                Procesado = false
            }).ToList();

            await _context.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return Result.SuccessWithMessage($"Se actualizó el Crédito {oCredito.ClaveCredito}.");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Error($"Error crítico al actualizar el crédito: {ex.Message}");
        }
    }
}
