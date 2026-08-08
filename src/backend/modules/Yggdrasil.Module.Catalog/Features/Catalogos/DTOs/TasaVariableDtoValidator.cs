namespace Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

public class TasaVariableDtoValidator : AbstractValidator<TasaVariableDto>
{
    public TasaVariableDtoValidator()
    {
        RuleFor(x => x.NomTasa)
            .NotEmpty().WithMessage("Requerido")
            .MaximumLength(100).WithMessage("debe ser < que 100 Car.");
    }
}
