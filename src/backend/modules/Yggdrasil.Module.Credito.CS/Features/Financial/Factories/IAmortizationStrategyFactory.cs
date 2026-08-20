using Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Amortization;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Factories;

public interface IAmortizationStrategyFactory
{
    IAmortizationStrategy GetStrategy(AmortizationMethod method);
    List<AmortizationMethod> GetAvailableMethods();
}

