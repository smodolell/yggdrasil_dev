namespace Yggdrasil.Domain.Entities;

public class OT_Persona
{
    public int Id { get; set; }
    public int SolicitudId { get; set; }
    public int TipoPersonaId { get; set; }
    public int? GeneroId { get; set; }

    public int? EdoCivilId { get; set; }

    [MaxLength(100)]
    public string Nombre { get; set; } = "";

    [MaxLength(80)]
    public string Apellido { get; set; } = "";

    [MaxLength(30)]
    public string? DNI { set; get; } = "";

    [MaxLength(30)]
    public string? CUIT { get; set; } = "";

    [MaxLength(100)]
    public string? RazonSocial { get; set; }

    public DateTime? FechaConstitucion { get; set; }


     [MaxLength(100)]
    public string? Calle { get; set; }

    [MaxLength(30)]
    public string? Numero { get; set; } = "";

    
    public short TiempoResidenciaDomicilio { get; set; }
    public short TiempoResidenciaCiudad { get; set; }


    [MaxLength(30)]
    public string? TelefonoCasa { get; set; }

    [MaxLength(30)]
    public string? TelefonoDomicilio { get; set; }

    [MaxLength(30)]
    public string? TelefonoCelular { get; set; }

    [MaxLength(150)]
    public string Email { get; set; } = "";

    [Column(TypeName = "Date")]
    public DateTime? FechaNacimiento { get; set; }


    public bool SostenFamiliar { get; set; }

    public short DependientesEconomicos { get; set; } = 0;

    [MaxLength(150)]
    public string? NombreCompletoConyuge { get; set; }

    [MaxLength(30)]
    public string? DNIConyuge { get; set; }

    [MaxLength(30)]
    public string? TelefonoConyuge { get; set; }

    [MaxLength(150)]
    public string? EmailConyuge { get; set; }



    public bool EsSolicitante { get; set; }
    public bool EsBeneficiario { get; set; }
    public bool EsAval { get; set; }
    public bool EsObligadoSolidario { get; set; }
    public bool EsRepresentateLegal { get; set; }



    [ForeignKey(nameof(TipoPersonaId))]
    public CAT_TipoPersona CAT_TipoPersona { get; set; } = null!;

    [ForeignKey(nameof(GeneroId))]
    public CAT_Genero? CAT_Genero { get; set; }

    [ForeignKey(nameof(EdoCivilId))]
    public CAT_EdoCivil? CAT_EdoCivil { get; set; }


    [ForeignKey(nameof(SolicitudId))]
    public OT_Solicitud OT_Solicitud { get; set; } = null!;


    //public ICollection<OT_OcupacionEmpleo> OT_OcupacionEmpleo { get; set; } = new HashSet<OT_OcupacionEmpleo>();
    //public ICollection<OT_Direccion> OT_Direccion { get; set; } = new HashSet<OT_Direccion>();
}
