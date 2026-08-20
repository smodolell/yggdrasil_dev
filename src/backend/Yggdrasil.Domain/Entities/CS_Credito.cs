namespace Yggdrasil.Domain.Entities;

public class CS_Credito
{
    public int Id { get; set; }
    public int TipoCreditoId { get; set; }
    public int EstatusCreditoId { get; set; }
    public int PeriodicidadId { get; set; }
    public int MetodoArmotizacionId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaPrimeraRenta { get; set; }
    public DateTime? FechaFirmaContrato { get; set; }
    public DateTime? FechaActivacion { get; set; }

    [Required]
    [MaxLength(30)]
    public string ClaveCredito { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(13, 2)")]
    public decimal Capital { get; set; }

    [Required]
    [Column(TypeName = "decimal(8, 6)")]
    public decimal Tasa { get; set; }

    [Required]
    [Column(TypeName = "decimal(8, 6)")]
    public decimal TasaIva { get; set; }
    public int Plazo { get; set; }

    public int VersionTabla { get; set; }

    [ForeignKey(nameof(TipoCreditoId))]
    public CS_TipoCredito CS_TipoCredito { get; set; } = null!;


    [ForeignKey(nameof(EstatusCreditoId))]
    public CS_EstatusCredito CS_EstatusCredito { get; set; } = null!;

    [ForeignKey(nameof(PeriodicidadId))]
    public CAT_Periodicidad CAT_Periodicidad { get; set; } = null!;

    [ForeignKey(nameof(MetodoArmotizacionId))]
    public CS_MetodoArmotizacion CS_MetodoArmotizacion { get; set; } = null!;

    public ICollection<CS_TablaAmortiza> CS_TablaAmortiza { get; set; } = new HashSet<CS_TablaAmortiza>();
}
