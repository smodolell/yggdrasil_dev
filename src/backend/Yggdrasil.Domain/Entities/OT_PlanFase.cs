namespace Yggdrasil.Domain.Entities;

public class OT_PlanFase
{
    public int PlanId { get; set; }
    public int FaseId { get; set; }

    public decimal Orden { get; set; }
    public OT_Plan OT_Plan { get; set; } = null!;
    public OT_Fase OT_Fase { get; set; } = null!;


}