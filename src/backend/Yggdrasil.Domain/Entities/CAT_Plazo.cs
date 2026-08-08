namespace Yggdrasil.Domain.Entities;

public class CAT_Plazo
{

    public int Id { get; set; }
    public int ValorPlazo { get; set; }
    public bool Activo { get; set; }



    //public ICollection<OT_PlanPlazo> OT_PlanPlazo { get; set; } = new HashSet<OT_PlanPlazo>();

    //public ICollection<OT_SolicitudPlazo> OT_SolicitudPlazo { get; set; } = new HashSet<OT_SolicitudPlazo>();

}
