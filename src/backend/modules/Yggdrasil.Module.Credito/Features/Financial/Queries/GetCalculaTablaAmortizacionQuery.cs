using Yggdrasil.Module.Credito.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.Features.Financial.Queries;

public record GetCalculaTablaAmortizacionQuery(AmortizationDto Model, AmortizationMethod Method) : IQuery<Result<AmortizationResultDto>>;

internal class GetCalculaTablaAmortizacionQueryHandler(IAmortizationStrategyFactory amortizationStrategyFactory, IValidator<AmortizationDto> validator) : IQueryHandler<GetCalculaTablaAmortizacionQuery, Result<AmortizationResultDto>>
{
    private readonly IAmortizationStrategyFactory _amortizationStrategyFactory = amortizationStrategyFactory;
    private readonly IValidator<AmortizationDto> _validator = validator;

    public async Task<Result<AmortizationResultDto>> HandleAsync(GetCalculaTablaAmortizacionQuery message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        return await Task.Run(() =>
        {
            var strategy = _amortizationStrategyFactory.GetStrategy(message.Method);

            
            var result = strategy.Calculate(model);

            result.Value.Method = message.Method;
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
        }, cancellationToken);
    }
}