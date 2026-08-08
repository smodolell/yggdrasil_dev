namespace Yggdrasil.Module.Cobranza.UI.Services.Catalogos.DTOs;

public class TipoPagoEditDto
{
    public int TipoPagoId { get; set; }
    public string NomTipoPago { get; set; } = "";
}

public class TipoPagoEditDtoValidator : AbstractValidator<TipoPagoEditDto>
{
    public TipoPagoEditDtoValidator()
    {
        RuleFor(r => r.NomTipoPago)
            .NotEmpty()
            .MaximumLength(80)
            .WithName("Tipo de Pago");
    }
}
