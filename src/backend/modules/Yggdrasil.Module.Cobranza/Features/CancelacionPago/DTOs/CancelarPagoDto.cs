namespace Yggdrasil.Module.Cobranza.Features.CancelacionPago.DTOs;

public class CancelarPagoItemDto
{
    public int PagoId { get; set; }
    public int MovimientoId { get; set; }
}

public class CancelarPagoDto
{
    public int Opcion { get; set; } = 1;
    public string MotivoCancelacion { get; set; } = "";
    public List<CancelarPagoItemDto> Pagos { get; set; } = new();
}

public class CancelarPagoDtoValidator : AbstractValidator<CancelarPagoDto>
{
    public CancelarPagoDtoValidator()
    {
        RuleFor(r => r.MotivoCancelacion)
            .NotEmpty()
            .MaximumLength(80)
            .WithName("Motivo de Cancelación");

        RuleFor(r => r.Pagos)
            .NotEmpty()
            .WithMessage("Debe indicar al menos un pago a cancelar.");
    }
}
