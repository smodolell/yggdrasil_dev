namespace Yggdrasil.Module.Credito.UI.Services.Configuacion.DTOs;

public class PerfilEditDtoValidator : AbstractValidator<PerfilEditDto>
{
    public PerfilEditDtoValidator()
    {
        RuleFor(r => r.NomPerfil).NotEmpty();
        RuleFor(r => r.Activo);
    }
}
