namespace Yggdrasil.Domain.Entities;

public class FI_Pago
{
    public int Id { get; set; }

    [Required]
    public int TipoPagoId { get; set; }

    [Required]
    public DateTime FechaRegistro { get; set; }

    [Required]
    public DateTime FechaPago { get; set; }

    [Required]
    public DateTime FechaModificacion { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal Monto { get; set; }


    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal SaldoFavor { get; set; }

    [Required]
    public bool Cancelado { get; set; }

    [Required]
    public bool Suspenso { get; set; }

    [Required]
    public bool Activo { get; set; }


    public Guid? CorrelationId { get; set; }

    public ICollection<FI_PagoMovimiento> FI_PagoMovimiento { get; set; } = new HashSet<FI_PagoMovimiento>();

    [ForeignKey(nameof(TipoPagoId))]
    public FI_TipoPago FI_TipoPago { get; set; } = null!;


}
