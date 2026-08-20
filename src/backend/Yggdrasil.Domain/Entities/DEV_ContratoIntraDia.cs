namespace Yggdrasil.Domain.Entities;

public class DEV_CreditoIntraDia
{
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Capital otorgado acumulado (Suma histórica de todas las disposiciones)
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal MontoOtorgado { get; set; }

    /// <summary>
    /// Saldo de Capital vivo (Sube con disposiciones, BAJA con pagos/prepagos)
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Capital { get; set; }

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal Tasa { get; set; }

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal TasaIva { get; set; }

    [Required]
    public DateTime FechaPrimeraRenta { get; set; }

    /// <summary>
    /// Estado del crédito: 1 = Activo, 2 = Cancelado/Liquidado, 3 = Bloqueado
    /// </summary>
    [Required]
    public int Estado { get; set; } = 1;

    // Propiedades de navegación
    [InverseProperty("DEV_CreditoIntraDia")]
    public virtual ICollection<DEV_MovimientoIntraDia> DEV_Movimientos { get; set; } = new HashSet<DEV_MovimientoIntraDia>();
    public virtual ICollection<DEV_TablaAmortiza> DEV_TablaAmortiza { get; set; } = new HashSet<DEV_TablaAmortiza>();

    // Bolsa actual de devengo (Relación 1:1)
    public virtual DEV_InteresAcumulado? DEV_InteresAcumulado { get; set; }
}

public class DEV_InteresAcumulado
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("DEV_CreditoIntraDia")]
    public Guid CreditoId { get; set; }

    [Required]
    public DateTime FechaInicio { get; set; }

    [Required]
    public DateTime FechaCalculo { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal SaldoCapital { get; set; }

    [Required]
    public int Dias { get; set; }

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal Tasa { get; set; }

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal TasaIva { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Interes { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Iva { get; set; }

    [Required]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    /// <summary>
    /// Propiedad calculada en memoria (No se persiste en DB para evitar inconsistencias)
    /// </summary>
    [NotMapped]
    public decimal SaldoInsoluto => (SaldoCapital + Interes + Iva);

    public virtual DEV_CreditoIntraDia DEV_CreditoIntraDia { get; set; } = null!;
}

public class DEV_MovimientoIntraDia
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("DEV_CreditoIntraDia")]
    public Guid CreditoId { get; set; }

    /// <summary>
    /// Consecutivo del movimiento dentro del crédito
    /// </summary>
    [Required]
    public int Nro { get; set; }

    [Required]
    [MaxLength(100)]
    public string Concepto { get; set; } = string.Empty;

    [Required]
    public DateTime Fecha { get; set; }

    // Imputación del movimiento
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Capital { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Interes { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Iva { get; set; }

    /// <summary>
    /// Foto exacta del Saldo Insoluto Total inmediatamente después de procesar este movimiento
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal SaldoInsolutoResultante { get; set; }

    [Required]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public virtual DEV_CreditoIntraDia DEV_CreditoIntraDia { get; set; } = null!;
}



public class DEV_TablaAmortiza
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }


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
    public Guid CreditoId { get; set; }

    [ForeignKey(nameof(CreditoId))]
    public DEV_CreditoIntraDia DEV_CreditoIntraDia { get; set; } = null!;


}


