using Yggdrasil.ApiClient.Contracts;

namespace Yggdrasil.Module.Credito.UI.Validators.Intradias;

public class CreditoEditDtoValidator : AbstractValidator<CreditoEditDto>
{
    public CreditoEditDtoValidator()
    {
        RuleFor(x => x.Capital)
            .GreaterThan(0)
            .WithName("Capital");

        RuleFor(x => x.Tasa)
            .GreaterThan(0)
            .WithName("Tasa");

        RuleFor(x => x.TasaIva)
            .GreaterThanOrEqualTo(0)
            .WithName("Tasa IVA");

        RuleFor(x => x.FechaPrimeraRenta)
            .NotEmpty()
            .WithName("Fecha de Primera Renta");
    }
}
