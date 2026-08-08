namespace Yggdrasil.Domain.Entities;

public class CAT_Periodicidad
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string ClavePeriodicidad { get; set; } = "";

    [Required]
    [MaxLength(60)]
    public string NomPeriodicidad { get; set; } = "";

    [Required]
    public short ParamDias { get; set; }

    [Required]
    public short ParamMes { get; set; }

    [Required]
    public short NroPagosAnio { get; set; } = 0;

    [Required]
    public short NroPagosMes { get; set; } = 0;

    public bool UsaDias { get; set; }
    public bool Activo { get; set; }

    //public ICollection<OT_PlanPeriodicidad> OT_PlanPeriodicidad { get; set; } = new HashSet<OT_PlanPeriodicidad>();
    //public ICollection<OT_SolicitudPeriodicidad> OT_SolicitudPeriodicidad { get; set; } = new HashSet<OT_SolicitudPeriodicidad>();

}
