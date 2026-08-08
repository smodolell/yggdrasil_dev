namespace Yggdrasil.Domain.Entities;

public class CAT_TipoPersona
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string NomTipoPersona { get; set; } = "";

    public bool Activo { get; set; }

    //public ICollection<OT_ImpresionTipoPersona> OT_ImpresionTipoPersona { get; set; } = new HashSet<OT_ImpresionTipoPersona>();
    //public ICollection<OT_PlanTipoPersona> OT_PlanTipoPersona { get; set; } = new HashSet<OT_PlanTipoPersona>();
    //public ICollection<OT_PlanDocumentacion> OT_PlanDocumentacion { get; set; } = new HashSet<OT_PlanDocumentacion>();
    //public ICollection<OT_PlanSeccion> OT_PlanSeccion { get; set; } = new HashSet<OT_PlanSeccion>();
}
