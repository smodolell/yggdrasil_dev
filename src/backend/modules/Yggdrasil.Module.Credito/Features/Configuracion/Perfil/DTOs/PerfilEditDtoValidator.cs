namespace Yggdrasil.Module.Credito.Features.Configuracion.Perfil.DTOs;

public class PerfilEditDtoValidator : AbstractValidator<PerfilEditDto>
{
    public PerfilEditDtoValidator()
    {
        RuleFor(r => r.NomPerfil).NotEmpty();
        RuleFor(r => r.Activo);
    }
}
