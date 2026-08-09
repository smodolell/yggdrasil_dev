using Yggdrasil.Module.Credito.Features.Financial.DTOs;

namespace Yggdrasil.Module.Credito.Features.Financial.Strategies;

public interface IAmortizationStrategy
{
    Result<AmortizationResultDto> Calculate(AmortizationDto request);
}

