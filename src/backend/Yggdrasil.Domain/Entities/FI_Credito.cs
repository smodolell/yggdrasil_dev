namespace Yggdrasil.Domain.Entities;

public class FI_Credito
{
    public int Id { get; set; }

    [Required]
    public int PersonaId { get; set; }

    [Required]
    public int ProductoId { get; set; }

    [Required]
    public int EstatusCreditoId { get; set; }

    [Required]
    public int MonedaId { get; set; }

    //public int? SolicitudId { get; set; }

    [Required]
    public DateTime FechaRegistro { get; set; }


    [Required]
    [MaxLength(30)]
    public string ClaveCredito { get; set; } = "";

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal Capital { get; set; }


    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal CapitalFinanciado { get; set; }

    public DateTime FechaAlta { get; set; }
    public DateTime? FechaPrimeraRenta { get; set; }

    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaActivacion { get; set; }

    public DateTime? FechaTerminacion { get; set; }

    [Required]
    [Column(TypeName = "decimal(8, 4)")]
    public decimal Tasa { get; set; } = 0.0m;

    [Required]
    [Column(TypeName = "decimal(8, 4)")]
    public decimal PuntosMas { get; set; } = 0.0m;

    [Required]
    [Column(TypeName = "decimal(8, 4)")]
    public decimal PuntosPor { get; set; } = 1.0m;

    [Required]
    [Column(TypeName = "decimal(8, 4)")]
    public decimal TasaBase { get; set; } = 0.0m;

    [Required]
    [Column(TypeName = "decimal(8, 4)")]
    public decimal TasaMora { get; set; } = 0.0m;

    [Required]
    [Column(TypeName = "decimal(8, 4)")]
    public decimal PuntosMasMora { get; set; } = 0.0m;

    [Required]
    [Column(TypeName = "decimal(8, 4)")]
    public decimal PuntosPorMora { get; set; } = 1.0m;

    [Required]
    [Column(TypeName = "decimal(8, 4)")]
    public decimal TasaBaseMora { get; set; } = 0.0m;

    [Required]
    [Column(TypeName = "decimal(8, 4)")]
    public decimal TasaIva { get; set; }

    [Required]
    public int Plazo { get; set; }

    [Required]
    public int PeriodicidadId { get; set; }

    [Required]
    public int VersionTabla { get; set; }

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal PagoMensual { get; set; }


    //[ForeignKey(nameof(SolicitudId))]
    //public OT_Solicitud? OT_Solicitud { get; set; }


    [ForeignKey(nameof(PersonaId))]
    public FI_Persona FI_Persona { get; set; } = null!;


    [ForeignKey(nameof(ProductoId))]
    public FI_Producto FI_Producto { get; set; } = null!;

    [ForeignKey(nameof(MonedaId))]
    public CAT_Moneda CAT_Moneda { get; set; } = null!;


    [ForeignKey(nameof(EstatusCreditoId))]
    public FI_EstatusCredito FI_EstatusCredito { get; set; } = null!;




    [ForeignKey(nameof(PeriodicidadId))]
    public CAT_Periodicidad CAT_Periodicidad { get; set; } = null!;

    public ICollection<FI_TablaAmortiza> FI_TablaAmortiza { get; set; } = new HashSet<FI_TablaAmortiza>();

    public ICollection<FI_Movimiento> FI_Movimiento { get; set; } = new HashSet<FI_Movimiento>();

    //public ICollection<FI_EstatusCredito> FI_CreditoStatus { get; set; } = new HashSet<FI_CreditoStatus>();
}
