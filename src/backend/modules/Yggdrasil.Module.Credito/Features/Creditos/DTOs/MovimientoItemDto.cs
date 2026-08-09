namespace Yggdrasil.Module.Credito.Features.Creditos.DTOs;

public class MovimientoItemDto
{
    public int Id { get; set; }
    public int NoPago { get; set; }
    public int TipoMovimientoId { get; set; }
    public string NomTipoMovimiento { get; set; } = "";
    public string DescMovimiento { get; set; } = "";
    public DateTime FechaRegistro { get; set; }
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
