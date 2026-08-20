namespace Yggdrasil.Module.Cobranza.Features.CajaManual.DTOs;

public class CajaManualDto
{
    public int PersonaId { get; set; }
    public int CreditoId { get; set; }
    public decimal Pago { get; set; }
    public int TipoPagoId { get; set; }
    public DateTime? FechaPago { get; set; }
    public DateTime? FechaMinima { get; set; }
    public List<CajaManualItemDto> Items { get; set; } = new List<CajaManualItemDto>();
}

public class CajaManualItemDto
{
    public int MovimientoId { get; set; }
    public int NoPago { get; set; }
    public string DescMovimiento { get; set; } = string.Empty;
    public DateTime FechaVencimiento { get; set; }
    public decimal Capital { get; set; }
    public decimal Interes { get; set; }
    public decimal Iva { get; set; }
    public decimal Total { get; set; }
    public decimal SaldoCapital { get; set; }
    public decimal SaldoInteres { get; set; }
    public decimal SaldoIva { get; set; }
    public decimal SaldoTotal { get; set; }
}
