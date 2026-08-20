namespace Yggdrasil.Domain.Entities;

public class CS_Movimiento
{
    public int Id { get; set; }

    [Required]
    public int TipoMovimientoId { get; set; }

    [Required]
    public int CreditoId { get; set; }

    [Required]
    [MaxLength(80)]
    public string DescMovimiento { get; set; } = "";

    [Required]
    public DateTime FechaRegistro { get; set; }

    [Required]
    public DateTime FechaVencimiento { get; set; }

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
    public decimal SaldoCapital { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal SaldoInteres { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal SaldoIva { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal SaldoTotal { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public int NoPago { get; set; }


    [ForeignKey(nameof(CreditoId))]
    public CS_Credito CS_Credito { get; set; } = null!;


    [ForeignKey(nameof(TipoMovimientoId))]
    public CS_TipoMovimiento CS_TipoMovimiento { get; set; } = null!;




    public ICollection<CS_PagoMovimiento> CS_PagoMovimiento { get; set; } = new HashSet<CS_PagoMovimiento>();


}
