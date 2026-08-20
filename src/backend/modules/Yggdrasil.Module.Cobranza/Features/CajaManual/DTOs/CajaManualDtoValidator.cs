namespace Yggdrasil.Module.Cobranza.Features.CajaManual.DTOs;

public class CajaManualDtoValidator : AbstractValidator<CajaManualDto>
{
    public CajaManualDtoValidator()
    {
        RuleFor(r => r.TipoPagoId)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithName("Tipo de Pago");

        RuleFor(r => r.FechaPago)
            .GreaterThanOrEqualTo(pago => pago.FechaMinima)
            .WithMessage("La Fecha de Pago no puede ser menor que la Fecha Mínima.")
            .LessThanOrEqualTo(DateTime.Today.AddDays(1))
            .WithMessage("La Fecha de Pago no puede ser mayor que la fecha de hoy.");

        RuleFor(r => r.Pago)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithName("Pago");
    }
}
