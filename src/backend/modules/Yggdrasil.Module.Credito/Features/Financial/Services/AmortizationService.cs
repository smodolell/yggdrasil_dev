using Yggdrasil.Module.Credito.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.Features.Financial.Services;

internal class AmortizationService(IAmortizationStrategyFactory amortizationStrategyFactory, IValidator<AmortizationDto> validator) : IAmortizationService
{
    private readonly IAmortizationStrategyFactory _amortizationStrategyFactory = amortizationStrategyFactory;
    private readonly IValidator<AmortizationDto> _validator = validator;

    public Result<AmortizationResultDto> Calculate(AmortizationDto model, AmortizationMethod method)
    {
        var validationResult = _validator.Validate(model);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        var strategy = _amortizationStrategyFactory.GetStrategy(method);


        var result = strategy.Calculate(model);

        result.Value.Method = method;
        result.Value.SaldoInicial = model.SaldoInicial;
        result.Value.Plazo = model.Plazo;
        result.Value.TasaAnual = model.TasaAnual;
        result.Value.GeneraIVAInteres = model.GeneraIVAInteres;
        result.Value.TasaIVA = (decimal)model.TasaIVA;
        result.Value.FechaInicio = model.FecInicioContrato;
        result.Value.FecPrimeraRenta = model.FecPrimeraRenta;


        var tableValidator = new AmortizationResultDtoValidator();
        var tableValidationResult = tableValidator.Validate(result.Value);

        if (!tableValidationResult.IsValid)
        {
            return Result.Invalid(tableValidationResult.AsErrors());
        }

        return result;

    }
}