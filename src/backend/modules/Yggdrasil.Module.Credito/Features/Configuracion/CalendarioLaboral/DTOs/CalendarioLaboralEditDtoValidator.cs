namespace Yggdrasil.Module.Credito.Features.Configuracion.CalendarioLaboral.DTOs;

public class CalendarioLaboralEditDtoValidator : AbstractValidator<CalendarioLaboralEditDto>
{
    public CalendarioLaboralEditDtoValidator()
    {
        RuleFor(x => x.Descripcion)
            .MaximumLength(200).WithMessage("La descripción no puede exceder los 200 caracteres");
    }
}
