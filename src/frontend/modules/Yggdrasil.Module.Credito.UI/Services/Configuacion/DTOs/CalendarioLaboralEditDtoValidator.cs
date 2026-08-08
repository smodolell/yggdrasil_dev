namespace Yggdrasil.Module.Credito.UI.Services.Configuacion.DTOs;

public class CalendarioLaboralEditDtoValidator : AbstractValidator<CalendarioLaboralEditDto>
{
    public CalendarioLaboralEditDtoValidator()
    {
        RuleFor(x => x.Descripcion)
            .MaximumLength(200).WithMessage("La descripción no puede exceder los 200 caracteres");
    }
}
