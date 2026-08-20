using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Factories;

public interface IFechasStrategiesFactory
{
    Task<List<DateTime>> GenerarCalendarioAjustadoAsync(
        AmortizationDto amortization,
        DateGenerationContext context
    );
}
