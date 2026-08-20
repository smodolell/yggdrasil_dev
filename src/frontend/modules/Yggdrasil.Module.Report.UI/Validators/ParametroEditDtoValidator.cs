using FluentValidation;
using Yggdrasil.ApiClient.Contracts;

namespace Yggdrasil.Module.Report.UI.Validators;

public class ParametroEditDtoValidator : AbstractValidator<ParametroEditDto>
{
    public ParametroEditDtoValidator()
    {
        RuleFor(r => r.NomParametro)
            .NotEmpty()
            .MaximumLength(80)
            .WithName("Nombre del Parametro");

        RuleFor(r => r.Display)
            .NotEmpty()
            .MaximumLength(80)
            .WithName("Display");
    }
}
