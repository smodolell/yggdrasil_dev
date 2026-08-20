namespace Yggdrasil.Domain.Entities;

public class OT_PlanPeriodicidad
{
    [Required]
    public int PlanId { get; set; }

    [Required]
    public int PeriodicidadId { get; set; }

    [ForeignKey(nameof(PlanId))]
    public OT_Plan OT_Plan { get; set; } = null!;

    [ForeignKey(nameof(PeriodicidadId))]
    public CAT_Periodicidad CAT_Periodicidad { get; set; } = null!;
}
