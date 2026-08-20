namespace Yggdrasil.Module.Cobranza.UI.Services.Cobranza.DTOs;

public class PagoDto
{
    public int CreditoId { get; set; }
    public int TipoPagoId { get; set; }
    public DateTime FechaPago { get; set; }
    public decimal Monto { get; set; }

    public List<int> Movimientos { get; set; } = new List<int>();
}

public class PagoResultDto
{
    public int PagoId { get; set; }
    public decimal MontoAplicado { get; set; }
    public decimal SaldoFavor { get; set; }
    public int MovimientosLiquidados { get; set; }
    public string Mensaje { get; set; } = string.Empty;
}

public class PagoDtoValidator : AbstractValidator<PagoDto>
{
    public PagoDtoValidator()
    {
        RuleFor(r => r.CreditoId)
            .GreaterThan(0)
            .WithName("Crédito");

        RuleFor(r => r.TipoPagoId)
            .GreaterThan(0)
            .WithName("Tipo de Pago");

        RuleFor(r => r.Monto)
            .GreaterThan(0)
            .WithName("Monto");

        RuleFor(r => r.FechaPago)
            .NotEmpty()
            .WithName("Fecha de Pago");

        RuleFor(r => r.Movimientos)
            .NotNull()
            .Must(m => m != null && m.Any())
            .WithMessage("Debe seleccionar al menos un movimiento")
            .WithName("Movimientos");
    }
}
