using Yggdrasil.Module.Credito.CS.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Attibutes;

[AttributeUsage(AttributeTargets.Class)]
public class AmortizationMethodAttribute : Attribute
{
    public AmortizationMethod Method { get; }
    public AmortizationMethodAttribute(AmortizationMethod method) => Method = method;
}
