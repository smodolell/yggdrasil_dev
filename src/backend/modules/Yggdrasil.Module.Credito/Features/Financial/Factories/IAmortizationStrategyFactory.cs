using Yggdrasil.Module.Credito.Features.Financial.Strategies;

namespace Yggdrasil.Module.Credito.Features.Financial.Factories;

public interface IAmortizationStrategyFactory
{
    IAmortizationStrategy GetStrategy(AmortizationMethod method);
    List<AmortizationMethod> GetAvailableMethods();
}

