namespace Yggdrasil.Module.Credito.CS.UI.Validators.Configuracion;

public class TipoCreditoCsEditDtoValidator : AbstractValidator<TipoCreditoCsEditDto>
{
    public TipoCreditoCsEditDtoValidator()
    {
        RuleFor(r => r.ClaveTipoCredito)
            .NotEmpty()
            .MaximumLength(20)
            .WithName("Clave");

        RuleFor(r => r.NomTipoCredito)
            .NotEmpty()
            .MaximumLength(80)
            .WithName("Nombre");
    }
}
