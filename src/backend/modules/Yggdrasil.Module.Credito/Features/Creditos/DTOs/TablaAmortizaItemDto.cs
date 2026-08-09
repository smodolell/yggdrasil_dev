namespace Yggdrasil.Module.Credito.Features.Creditos.DTOs;

public class TablaAmortizaItemDto
{
    public Guid Id { get; set; }
    public int NoPago { get; set; }
    public DateTime FechaInicial { get; set; }
    public DateTime FechaFinal { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public int Dias { get; set; }
    public decimal SaldoInicial { get; set; }
    public decimal Capital { get; set; }
    public decimal Interes { get; set; }
    public decimal Iva { get; set; }
    public decimal Total { get; set; }
    public decimal SaldoFinal { get; set; }
    public decimal TasaCalculo { get; set; }
    public bool Procesado { get; set; }
    public int VersionTabla { get; set; }
    public int TipoMovimientoId { get; set; }
    public string NomTipoMovimiento { get; set; } = "";
}
