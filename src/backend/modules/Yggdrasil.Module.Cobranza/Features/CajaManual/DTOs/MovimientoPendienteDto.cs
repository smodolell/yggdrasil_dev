namespace Yggdrasil.Module.Cobranza.Features.CajaManual.DTOs;

public class MovimientoPendienteDto
{
    public int Id { get; set; }
    public int CreditoId { get; set; }
    public int NoPago { get; set; }
    public string DescMovimiento { get; set; } = "";
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
