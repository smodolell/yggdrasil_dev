namespace Yggdrasil.Module.Auth.Features.Rol.DTOs;

public class RolUpdateDto
{
    public int RolId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public class RolUpdateDtoValidator : AbstractValidator<RolUpdateDto>
{
    public RolUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithName("Nombre");

        RuleFor(x => x.Descripcion)
            .NotEmpty()
            .WithName("Descripción");
    }
}
