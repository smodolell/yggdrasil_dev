using Yggdrasil.Module.Credito.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.Features.Financial.Attibutes;

[AttributeUsage(AttributeTargets.Class)]
public class AmortizationMethodAttribute : Attribute
{
    public AmortizationMethod Method { get; }
    public AmortizationMethodAttribute(AmortizationMethod method) => Method = method;
}
