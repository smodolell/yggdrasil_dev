namespace Yggdrasil.Domain.Entities;

public class FI_TablaAmortiza
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public int TipoMovimientoId { get; set; }


    [Required]
    public DateTime FechaInicial { get; set; }

    [Required]
    public DateTime FechaFinal { get; set; }

    [Required]
    public DateTime FechaVencimiento { get; set; }

    [Required]
    public int NoPago { get; set; }

    [Required]
    public int Dias { get; set; }
    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal SaldoInicial { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal Capital { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal Interes { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal Iva { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal Total { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal SaldoFinal { get; set; }
    [Required]
    [Column(TypeName = "decimal(8, 4)")]
    public decimal TasaCalculo { get; set; }
    [Required]
    public bool Procesado { get; set; }

    [Required]
    public int VersionTabla { get; set; }

    [Required]
    public int CreditoId { get; set; }

    [ForeignKey(nameof(CreditoId))]
    public FI_Credito FI_Credito { get; set; } = null!;

    [ForeignKey(nameof(TipoMovimientoId))]
    public FI_TipoMovimiento FI_TipoMovimiento { get; set; } = null!;

}
