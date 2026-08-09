namespace Yggdrasil.Module.Credito.UI.Services.Creditos.DTOs;

public class PagoDetailDto
{
    public int PagoId { get; set; }
    public DateTime FechaRegistro { get; set; }
    public DateTime FechaPago { get; set; }
    public DateTime FechaModificacion { get; set; }
    public decimal Monto { get; set; }
    public decimal SaldoFavor { get; set; }

    public List<PagoDetailItemDto> Detalles { get; set; } = [];
}

public class PagoDetailItemDto
{
    public int MovimientoId { get; set; }
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

    public decimal TotalPagado { get; set; }
    public decimal CapitalPagado { get; set; }
    public decimal InteresPagado { get; set; }
    public decimal IvaPagado { get; set; }
    public DateTime FechaPago { get; set; }
    public bool Cancelado { get; set; }
    public string MotivoCancelacion { get; set; } = "";
}
