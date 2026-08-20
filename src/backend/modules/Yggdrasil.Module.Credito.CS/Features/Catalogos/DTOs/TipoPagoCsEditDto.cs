namespace Yggdrasil.Module.Credito.CS.Features.Catalogos.DTOs;

public class TipoPagoCsEditDto
{
    public string NomTipoPago { get; set; } = "";
}

public class TipoPagoCsEditDtoValidator : AbstractValidator<TipoPagoCsEditDto>
{
    public TipoPagoCsEditDtoValidator()
    {
        RuleFor(x => x.NomTipoPago)
            .NotEmpty()
            .MaximumLength(100)
            .WithName("Nombre del Tipo de Pago");
    }
}
