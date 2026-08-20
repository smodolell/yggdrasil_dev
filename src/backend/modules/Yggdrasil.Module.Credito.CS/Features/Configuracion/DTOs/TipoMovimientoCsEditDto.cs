namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;

public class TipoMovimientoCsEditDto
{
    public string Clave { get; set; } = "";
    public string NomTipoMovimiento { get; set; } = "";
    public bool Activo { get; set; } = true;
}

public class TipoMovimientoCsEditDtoValidator : AbstractValidator<TipoMovimientoCsEditDto>
{
    public TipoMovimientoCsEditDtoValidator()
    {
        RuleFor(x => x.Clave)
            .NotEmpty()
            .MaximumLength(6)
            .WithName("Clave");

        RuleFor(x => x.NomTipoMovimiento)
            .NotEmpty()
            .MaximumLength(60)
            .WithName("Nombre del Tipo de Movimiento");
    }
}
