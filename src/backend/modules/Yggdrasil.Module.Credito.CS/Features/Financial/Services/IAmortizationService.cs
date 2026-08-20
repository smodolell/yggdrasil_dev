using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Services;

public interface IAmortizationService
{
    Task<Result<AmortizationResultDto>> CalculateAsync(AmortizationDto model, AmortizationMethod method);
    Task<Result<AmortizationResultDto>> CalculateAsync(AmortizationDto model, AmortizationMethod method, List<DateTime> fechas);
}
