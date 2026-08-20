using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Amortization;

public interface IAmortizationStrategy
{
    Result<AmortizationResultDto> Calculate(AmortizationDto request,List<DateTime> fechas);
}

