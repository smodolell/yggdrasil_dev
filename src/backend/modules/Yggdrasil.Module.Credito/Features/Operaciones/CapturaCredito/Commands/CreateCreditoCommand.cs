using Yggdrasil.Module.Credito.Features.Financial.DTOs;
using Yggdrasil.Common.Attributes;
using Yggdrasil.Module.Credito.Features.Financial.Services;
using Yggdrasil.Module.Credito.Features.Financial.Factories;
using Yggdrasil.Common.Constants;
using Yggdrasil.Module.Credito.Features.Operaciones.CapturaCredito.DTOs;

namespace Yggdrasil.Module.Credito.Features.Operaciones.CapturaCredito.Commands;

[Auditable(AuditEvents.CrearCredito)]
public record CreateCreditoCommand(CreditoEditDto Model) : ICommand<Result<int>>;

internal class CreateCreditoCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork,
    IMapper mapper, IValidator<CreditoEditDto> validator, IAmortizationService amortizationService) : ICommandHandler<CreateCreditoCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreditoEditDto> _validator = validator;
    private readonly IAmortizationService _amortizationService = amortizationService;

    public async Task<Result<int>> HandleAsync(CreateCreditoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;

        // 1. Validaciones previas (Fuera de la lógica transaccional dura)
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        var oPersona = await _context.FI_Persona
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == model.PersonaId, cancellationToken);
        if (oPersona == null) return Result.NotFound("Cliente no existe");

        var oPeriodicidad = await _context.CAT_Periodicidad.SingleOrDefaultAsync(r => r.Id == model.PeriodicidadId, cancellationToken);
        if (oPeriodicidad == null)
            return Result.Invalid(new ValidationError("Periodicidad no existe"));

        var oProducto = await _context.FI_Producto.SingleOrDefaultAsync(r => r.Id == model.ProductoId, cancellationToken);
        if (oProducto == null)
            return Result.Invalid(new ValidationError("Se requiere el Producto"));

        var oMovimientoRenta = await _context.FI_TipoMovimiento.SingleOrDefaultAsync(r => r.Id == oProducto.TipoMovimientoRentaId, cancellationToken);
        if (oMovimientoRenta == null)
            return Result.Invalid(new ValidationError("Configure el tipo de movimiento Renta"));

        // 2. Cálculo Financiero de la tabla de amortización
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

        // 3. Inicio de la sección crítica: Modificación y Persistencia
        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            oProducto.Consecutivo++;
            var claveCredito = GenerarClaveCredito(oProducto);

            var oCredito = new FI_Credito
            {
                PersonaId = oPersona.Id,
                FechaRegistro = model.FechaRegistro ?? DateTime.Now,
                EstatusCreditoId = AppConstants.CAT_EstatusCreditoId_CAPTURADO,
                PeriodicidadId = model.PeriodicidadId!.Value,
                ProductoId = model.ProductoId,
                MonedaId = model.MonedaId!.Value,
                ClaveCredito = claveCredito,
                Capital = model.CapitalFinanciado,
                FechaActivacion = null,
                VersionTabla = 1
            };
            await _context.FI_Credito.AddAsync(oCredito);

            _mapper.Map(model, oCredito);

   

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

            // Guardado y confirmación atómica de la operación

            await _context.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return Result.Success(oCredito.Id, $"Se Capturo el Crédito {oCredito.ClaveCredito}.");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Error($"Error crítico al persistir el crédito: {ex.Message}");
        }
    }

    private static string GenerarClaveCredito(FI_Producto producto)
    {
        var prefijo = producto.Prefijo ?? "CR";
        var sufijo = producto.Posfijo ?? "";
        var contador = producto.Consecutivo.ToString().PadLeft(8, '0');
        return $"{prefijo}{contador}{sufijo}".Trim();
    }
}