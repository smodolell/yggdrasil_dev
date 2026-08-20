using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Financial.Factories;
using Yggdrasil.Module.Credito.CS.Features.Financial.Services;
using Yggdrasil.Module.Credito.CS.Features.Operaciones.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Operaciones.Commands;

public record CapturarCreditoCommand(int IdCredito, CreditoCSEditDto Model) : ICommand<Result<int>>;


internal class CapturarCreditoCommandHandler(
    IApplicationDbContext context,
    IUnitOfWork unitOfWork,
    IValidator<CreditoCSEditDto> validator,
        IAmortizationService amortizationService
) : ICommandHandler<CapturarCreditoCommand, Result<int>>

{
    private readonly IApplicationDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<CreditoCSEditDto> _validator = validator;
    private readonly IAmortizationService _amortizationService = amortizationService;

    public async Task<Result<int>> HandleAsync(CapturarCreditoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var idCredito = message.IdCredito;
        // 1. Validar con FluentValidation
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<int>.Invalid(validationResult.AsErrors());
        }


        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var periodicidad = await _context.CAT_Periodicidad
             .SingleOrDefaultAsync(r => r.Id == model.PeriodicidadId, cancellationToken);
            if (periodicidad == null)
            {
                return Result.Invalid(new ValidationError("Periodicidad no válida"));
            }

            var tipoCredito = await _context.CS_TipoCredito
              .SingleOrDefaultAsync(r => r.Id == model.TipoCreditoId, cancellationToken);
            if (tipoCredito == null)
            {
                return Result.Invalid(new ValidationError("Tipo de crédito no válido"));
            }

            CS_Credito? credito;
            bool esNuevo = false;
            if (idCredito == 0)
            {
                credito = new CS_Credito { VersionTabla = 1 };
                esNuevo = true;
                _context.CS_Credito.Add(credito);
            }
            else
            {
                credito = await _context.CS_Credito
                .Include(c => c.CS_TablaAmortiza)
                .SingleOrDefaultAsync(c => c.Id == idCredito, cancellationToken);

                if (credito == null)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result<int>.Invalid(new ValidationError("No existe el contrato a actualizar"));
                }
            }

            MapCredito(model, credito, tipoCredito, esNuevo);

            bool requiereRecalcularTabla = !model.EsImportacionExcel ||
                               (model.ExcelFileBytes != null && model.ExcelFileBytes.Length > 0);

            if (requiereRecalcularTabla)
            {
                var taParams = ParamsTA(credito, periodicidad, model);
                var method = (AmortizationMethod)model.MetodoArmotizacionId;

                // 1. Calcular/Procesar
                var tablaAmortizaResult = await _amortizationService.CalculateAsync(taParams, method);

                if (!tablaAmortizaResult.IsSuccess)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return Result.Invalid(tablaAmortizaResult.ValidationErrors);
                }

                // 2. Eliminar la tabla anterior SOLO si el cálculo fue exitoso y se requiere actualizar
                if (credito.CS_TablaAmortiza.Any())
                {
                    _context.CS_TablaAmortiza.RemoveRange(credito.CS_TablaAmortiza);
                }

                await _unitOfWork.SaveAsync(cancellationToken);

                // 3. Mapear e insertar las nuevas cuotas
                MapTablaAmortiza(credito, tablaAmortizaResult.Value);
            }

            await _unitOfWork.SaveAsync(cancellationToken);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return Result.Success(credito.Id);
        }
        catch (DbUpdateException ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            // Log del error
            return Result.Error($"Error al guardar el contrato: {ex.Message}");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            // Log del error
            return Result<int>.Error($"Error inesperado: {ex.Message}");
        }
    }






    private static void MapCredito(CreditoCSEditDto model, CS_Credito entity, CS_TipoCredito tipoCredito, bool esNuevo)
    {
        entity.EstatusCreditoId = 1;//CAPATURADO
        entity.TipoCreditoId = model.TipoCreditoId;
        entity.PeriodicidadId = model.PeriodicidadId;
        entity.MetodoArmotizacionId = model.MetodoArmotizacionId;
        entity.FechaInicio = model.FechaInicio;
        entity.FechaPrimeraRenta = model.FechaPrimeraRenta;
        entity.FechaFirmaContrato = model.FechaFirmaContrato;
        entity.Capital = model.Capital;
        entity.Tasa = model.Tasa;
        entity.TasaIva = model.TasaIva;
        entity.Plazo = model.Plazo;

        entity.VersionTabla = 1;

        if (esNuevo)
        {
            entity.FechaActivacion = null;
            tipoCredito.Consecutivo++;
            var claveCredito = string.Format(
                "{0}{1}{2}",
                tipoCredito.Prefijo,
                tipoCredito.Postfijo,
                (tipoCredito.Consecutivo + 1).ToString().PadLeft(5, '0')
            );

            entity.ClaveCredito = claveCredito;
        }
    }

    private void MapTablaAmortiza(CS_Credito contrato, AmortizationResultDto amortization)
    {


        contrato.CS_TablaAmortiza = amortization.TablaAmortiza.Select(s => new CS_TablaAmortiza
        {
            CreditoId = contrato.Id,
            TipoMovimientoId = s.IdTipoTabla,
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
            VersionTabla = contrato.VersionTabla,
            TasaCalculo = contrato.Tasa,
            Procesado = false,
        }).ToList();


    }
    private AmortizationDto ParamsTA(
    CS_Credito contrato,
    CAT_Periodicidad periodicidad,
    CreditoCSEditDto data)
    {
        var model = new AmortizationDto
        {
            SaldoInicial = contrato.Capital,
            Plazo = contrato.Plazo,
            UsaDias = periodicidad.UsaDias,
            ParamMes = periodicidad.ParamMes,
            ParamDias = periodicidad.ParamDias,
            FecInicioContrato = contrato.FechaInicio,
            FecPrimeraRenta = contrato.FechaPrimeraRenta,
            TasaAnual = Convert.ToDouble(contrato.Tasa / 100.0m),
            TasaIVA = (double)contrato.TasaIva / 100.0,
            //PeriodosGracia = data.PeriodosGracia,
            EsImportacionExcel = data.EsImportacionExcel,
            ExcelFileBytes = data.ExcelFileBytes,
            NombreArchivoExcel = data.NombreArchivoExcel,
            FechaFirmaContrato = contrato.FechaFirmaContrato!.Value,

        };

        return model;


    }
}
