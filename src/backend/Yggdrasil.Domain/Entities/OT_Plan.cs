namespace Yggdrasil.Domain.Entities;

public class OT_Plan
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    [Required]
    [MaxLength(100)]
    public string NomPlan { get; set; } = "";

    [Required]
    [MaxLength(200)]
    public string DescPlan { get; set; } = "";

    [Column(TypeName = "decimal(13, 2)")]
    public decimal ImporteMinimo { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal ImporteMaximo { get; set; }

    public bool GraciaCapital { get; set; }
    public bool GraciaInteres { get; set; }

    [Required]
    [Column(TypeName = "decimal(8, 4)")]
    public decimal TasaIvaConRFC { get; set; }

    [Required]
    [Column(TypeName = "decimal(8, 4)")]
    public decimal TasaIvaSinRFC { get; set; }

    public int EdadMinima { get; set; }
    public int EdadMaxima { get; set; }



    public bool Activo { get; set; }


    [ForeignKey(nameof(ProductoId))]
    public FI_Producto FI_Producto { get; set; } = null!;
    
    public ICollection<OT_PlanPeriodicidad> OT_PlanPeriodicidad { get; set; } = new HashSet<OT_PlanPeriodicidad>();
    public ICollection<OT_PlanPlazo> OT_PlanPlazo { get; set; } = new HashSet<OT_PlanPlazo>();

    //public ICollection<OT_PlanDocumentacion> OT_PlanDocumentacion { get; set; } = new HashSet<OT_PlanDocumentacion>();
    //public ICollection<OT_PlanImpresion> OT_PlanImpresion { get; set; } = new HashSet<OT_PlanImpresion>();
    //public ICollection<OT_PlanTipoPersona> OT_PlanTipoPersona { get; set; } = new HashSet<OT_PlanTipoPersona>();
    //public ICollection<OT_PlanSeccion> OT_PlanSeccion { get; set; } = new HashSet<OT_PlanSeccion>();
    public ICollection<OT_PlanFase> OT_PlanFase { get; set; } = new HashSet<OT_PlanFase>();
//    public ICollection<OT_PlanDestinoFinanciamiento> OT_PlanDestinoFinanciamiento { get; set; } = new HashSet<OT_PlanDestinoFinanciamiento>();
}