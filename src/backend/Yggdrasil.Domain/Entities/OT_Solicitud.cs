namespace Yggdrasil.Domain.Entities;

public class OT_Solicitud
{
    public int Id { get; set; }
    //public int UsuarioId { get; set; }
    public int FaseEstadoId { get; set; }
    //public int? PerfilId { get; set; }

    public int? PlanId { get; set; }
    public int? ProductoId { get; set; }
    public int? TipoPersonaId { get; set; }
    public int? BancoId { get; set; }


    public int? AsesorId { get; set; }
    public int? AnalistaId { get; set; }
    public int? SucursalId { get; set; }


    [Required]
    public DateTime FechaRegistro { get; set; }


    [Column(TypeName = "decimal(13, 2)")]
    public decimal ImporteMinimo { get; set; }

    [Column(TypeName = "decimal(13, 2)")]
    public decimal ImporteMaximo { get; set; }


    [MaxLength(100)]
    public string? DestinoCredito { get; set; }


    [Column(TypeName = "decimal(13, 2)")]
    public decimal MontoSolicitado { get; set; }


    [MaxLength(30)]
    public string? CBU { get; set; }

    [Required]
    public bool Activa { get; set; } = false;


    //[ForeignKey(nameof(UsuarioId))]
    //public CLI_Usuario CLI_Usuario { get; set; } = null!;    
    
    [ForeignKey(nameof(FaseEstadoId))]
    public OT_FaseEstado OT_FaseEstado { get; set; } = null!;

    [ForeignKey(nameof(ProductoId))]
    public FI_Producto? FI_Producto { get; set; }

    [ForeignKey(nameof(TipoPersonaId))]
    public CAT_TipoPersona? CAT_TipoPersona { get; set; }


    //[ForeignKey(nameof(PerfilId))]
    //public OT_Perfil? OT_Perfil { get; set; }


    [ForeignKey(nameof(PlanId))]
    public OT_Plan? OT_Plan{ get; set; }

    [ForeignKey(nameof(BancoId))]
    public CAT_Banco? CAT_Banco { get; set; }


  
    public ICollection<OT_Persona> OT_Persona { get; set; } = new HashSet<OT_Persona>();
    public ICollection<OT_SolicitudFase> OT_SolicitudFase { get; set; } = new HashSet<OT_SolicitudFase>();
    //public ICollection<OT_SolicitudPlazo> OT_SolicitudPlazo { get; set; } = new HashSet<OT_SolicitudPlazo>();
    //public ICollection<OT_SolicitudPeriodicidad> OT_SolicitudPeriodicidad { get; set; } = new HashSet<OT_SolicitudPeriodicidad>();
    //public ICollection<OT_SolicitudPeriodoGracia> OT_SolicitudPeriodoGracia { get; set; } = new HashSet<OT_SolicitudPeriodoGracia>();
    //public ICollection<OT_SolicitudImpresion> OT_SolicitudImpresion { get; set; } = new HashSet<OT_SolicitudImpresion>();
    //public ICollection<OT_SolicitudDestinoFinanciamiento> OT_SolicitudDestinoFinanciamiento { get; set; } = new HashSet<OT_SolicitudDestinoFinanciamiento>();
    //public ICollection<OT_Cuestionario> OT_Cuestionario { get; set; } = new HashSet<OT_Cuestionario>();
    //public ICollection<OT_Referencia> OT_Referencia { get; set; } = new HashSet<OT_Referencia>();
    //public ICollection<OT_PersonaAsociada> OT_PersonaAsociada { get; set; } = new HashSet<OT_PersonaAsociada>();



    [ForeignKey(nameof(AsesorId))]
    public SYS_Usuario? SYS_Usuario_Asesor { get; set; }

    [ForeignKey(nameof(AnalistaId))]
    public SYS_Usuario? SYS_Usuario_Analista { get; set; }

    //[ForeignKey(nameof(SucursalId))]
    //public CAT_Sucursal? CAT_Sucursal { get; set; }




}
