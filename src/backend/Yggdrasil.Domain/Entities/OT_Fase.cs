namespace Yggdrasil.Domain.Entities;

public class OT_Fase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(60)]
    public string ClaveFase { get; set; } = "";

    [Required]
    [MaxLength(100)]
    public string NomFase { get; set; } = "";

    [Required]
    [MaxLength(60)]
    public string MapRoute { get; set; } = "";

    public bool EsInicial { get; set; }
    public bool EsFinal { get; set; }
    public bool InClient { get; set; }
    public bool Required { get; set; }
    public decimal Orden { get; set; }

    public ICollection<OT_FaseEstado> OT_FaseEstado { get; set; } = new HashSet<OT_FaseEstado>();
    public ICollection<OT_SolicitudFase> OT_SolicitudFase { get; set; } = new HashSet<OT_SolicitudFase>();
    public ICollection<OT_PlanFase> OT_PlanFase { get; set; } = new HashSet<OT_PlanFase>();
}