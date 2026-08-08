namespace Yggdrasil.Domain.Entities;

public class FI_PagoMovimiento
{
    [Required]
    public int PagoId { get; set; }

    [Required]
    public int MovimientoId { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal TotalPagado { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal CapitalPagado { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal InteresPagado { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal IvaPagado { get; set; }

    [Required]
    public DateTime FechaPago { get; set; }

    [Required]
    public bool Cancelado { get; set; }

    [MaxLength(100)]
    public string MotivoCancelacion { get; set; } = "";

    [Required]
    public bool Activo { get; set; }

    [ForeignKey(nameof(PagoId))]
    public FI_Pago FI_Pago { get; set; } = null!;

    [ForeignKey(nameof(MovimientoId))]
    public FI_Movimiento FI_Movimiento { get; set; } = null!;
}

