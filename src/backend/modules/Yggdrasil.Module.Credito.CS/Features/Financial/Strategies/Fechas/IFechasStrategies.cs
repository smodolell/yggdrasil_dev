using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Fechas;

public interface IFechasStrategies
{
    Task<List<DateTime>> GenerarCalendarioFechasAsync(AmortizationDto amortization);
}
