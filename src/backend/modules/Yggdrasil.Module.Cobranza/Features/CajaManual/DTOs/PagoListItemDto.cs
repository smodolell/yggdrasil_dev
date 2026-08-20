namespace Yggdrasil.Module.Cobranza.Features.CajaManual.DTOs;

public class PagoListItemDto
{
    public int PagoId { get; set; }
    public DateTime FechaRegistro { get; set; }
    public string ClaveCredito { get; set; } = "";
    public string NomTipoPago { get; set; } = "";
    public DateTime? FechaPago { get; set; }
    public decimal Monto { get; set; }
    public decimal MontoAplicado { get; set; }
    public decimal? SaldoFavor { get; set; }
}
