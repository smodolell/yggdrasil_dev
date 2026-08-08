namespace Yggdrasil.Module.Credito.Features.Clientes.DTOs;

public class PersonaFisicaEditDtoValidator : AbstractValidator<PersonaFisicaEditDto>
{
    public PersonaFisicaEditDtoValidator()
    {
        RuleFor(r => r.GeneroId).NotNull().GreaterThan(0);
        RuleFor(r => r.EdoCivilId).NotNull().GreaterThan(0);
        RuleFor(r => r.LugarNacimientoId).NotEmpty();
        RuleFor(r => r.PrimerNombre)
            .NotEmpty()
            .MaximumLength(80);
        RuleFor(r => r.ApellidoPaterno).NotEmpty();
        RuleFor(r => r.ApellidoMaterno).NotEmpty();
        RuleFor(r => r.RFC).NotEmpty();
        RuleFor(r => r.CURP).NotEmpty();
        RuleFor(r => r.FechaNacimiento);
        RuleFor(r => r.Email).NotEmpty();
    }


}
