namespace Yggdrasil.Module.Credito.Features.Creditos.DTOs;

public class PagoItemDto
{
    public int Id { get; set; }
    public int TipoPagoId { get; set; }
    public string NomTipoPago { get; set; } = "";
    public DateTime FechaRegistro { get; set; }
    public DateTime FechaPago { get; set; }
    public DateTime FechaModificacion { get; set; }
    public decimal Monto { get; set; }
    public decimal SaldoFavor { get; set; }
    public bool Cancelado { get; set; }
    public bool Suspenso { get; set; }
    public bool Activo { get; set; }
    public Guid? CorrelationId { get; set; }
    public List<PagoMovimientoDetailDto> Movimientos { get; set; } = [];
}

public class PagoMovimientoDetailDto
{
    public int MovimientoId { get; set; }
    public DateTime FechaPago { get; set; }
    public decimal TotalPagado { get; set; }
    public decimal CapitalPagado { get; set; }
    public decimal InteresPagado { get; set; }
    public decimal IvaPagado { get; set; }
    public bool Cancelado { get; set; }
    public string MotivoCancelacion { get; set; } = "";
}
