using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Services;

internal class AmortizationService(
    IAmortizationStrategyFactory amortizationStrategyFactory,
    IFechasStrategiesFactory fechasStrategiesFactory,
    IValidator<AmortizationDto> validator) : IAmortizationService
{
    private readonly IAmortizationStrategyFactory _amortizationStrategyFactory = amortizationStrategyFactory;
    private readonly IFechasStrategiesFactory _fechasStrategiesFactory = fechasStrategiesFactory;
    private readonly IValidator<AmortizationDto> _validator = validator;

    /// <summary>
    /// Sobrecarga 1: Genera el calendario de fechas dinámicamente (Originación nueva)
    /// </summary>
    public async Task<Result<AmortizationResultDto>> CalculateAsync(AmortizationDto model, AmortizationMethod method)
    {
        var validationResult = await _validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        // Generamos las fechas usando el Factory de estrategias de fechas
        try
        {
            var fechas = await _fechasStrategiesFactory.GenerarCalendarioAjustadoAsync(model, new DateGenerationContext("model.Fondeador"));
            return ValidarYCalcularInterno(model, method, fechas);            
        }
        catch (Exception ex)
        {

            return Result.Invalid(new ValidationError(ex.Message));
        }
        

        
    }

    /// <summary>
    /// Sobrecarga 2: Recibe una lista de fechas preexistentes (Ideal para Cambio de Tasa / Reestructuras)
    /// </summary>
    public async Task<Result<AmortizationResultDto>> CalculateAsync(AmortizationDto model, AmortizationMethod method, List<DateTime> fechas)
    {
        var validationResult = await _validator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        if (fechas == null || fechas.Count == 0)
        {
            return Result.Invalid(new ValidationError("La lista de fechas provista no puede estar vacía."));
        }

        return ValidarYCalcularInterno(model, method, fechas);
    }

    /// <summary>
    /// Método privado para centralizar las asignaciones, el cálculo matemático y la validación final de la tabla
    /// </summary>
    private Result<AmortizationResultDto> ValidarYCalcularInterno(AmortizationDto model, AmortizationMethod method, List<DateTime> fechas)
    {
        var strategy = _amortizationStrategyFactory.GetStrategy(method);

        // Ejecución del motor matemático síncrono
        var result = strategy.Calculate(model, fechas);

        if (!result.IsSuccess)
        {
            return result;
        }

        // Enriquecemos el DTO de salida con los metadatos de la petición
        result.Value.Method = method;
        result.Value.SaldoInicial = model.SaldoInicial;
        result.Value.Plazo = model.Plazo;
        result.Value.TasaAnual = model.TasaAnual;
        result.Value.GeneraIVAInteres = model.GeneraIVAInteres;
        result.Value.TasaIVA = (decimal)model.TasaIVA;
        result.Value.FechaInicio = model.FecInicioContrato;
        result.Value.FecPrimeraRenta = model.FecPrimeraRenta;

        // Validación Post-Cálculo con el FluentValidator adaptado a Gracia
        var tableValidator = new AmortizationResultDtoValidator();
        var tableValidationResult = tableValidator.Validate(result.Value);

        if (!tableValidationResult.IsValid)
        {
            return Result.Invalid(tableValidationResult.AsErrors());
        }

        return result;
    }
}