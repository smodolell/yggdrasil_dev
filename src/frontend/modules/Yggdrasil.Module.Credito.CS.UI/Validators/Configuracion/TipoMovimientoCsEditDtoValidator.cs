namespace Yggdrasil.Module.Credito.CS.UI.Validators.Configuracion;

public class TipoMovimientoCsEditDtoValidator : AbstractValidator<TipoMovimientoCsEditDto>
{
    public TipoMovimientoCsEditDtoValidator()
    {
        RuleFor(r => r.Clave)
            .NotEmpty()
            .MaximumLength(20)
            .WithName("Clave");

        RuleFor(r => r.NomTipoMovimiento)
            .NotEmpty()
            .MaximumLength(80)
            .WithName("Nombre");
    }
}
