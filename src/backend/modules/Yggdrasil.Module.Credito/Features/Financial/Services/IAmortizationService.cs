using Yggdrasil.Module.Credito.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.Features.Financial.Services;

public interface IAmortizationService
{
    Result<AmortizationResultDto> Calculate(AmortizationDto model, AmortizationMethod method);

}
