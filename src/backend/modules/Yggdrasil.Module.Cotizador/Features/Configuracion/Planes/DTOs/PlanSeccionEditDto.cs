namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

public class PlanSeccionEditDto
{
    public int? PlanId { get; set; }
    public string NomPlan { get; set; } = string.Empty;
    public string NomTipoPersona { get; set; } = string.Empty;

    public bool PF { get; set; }
    public bool PJ { get; set; }

    public List<SeccionEditDto> Items { get; set; } = new List<SeccionEditDto>();
}

public class SeccionEditDto
{
    public int SeccionId { get; set; }
    public int PlanId { get; set; }
    public string NomSeccion { get; set; } = "";
    public bool PF { get; set; }
    public bool PFAE { get; set; }
    public bool PM { get; set; }


    public bool Required { get; set; }

    public bool PF_Selected { get; set; }
    public bool PFAE_Selected { get; set; }
    public bool PM_Selected { get; set; }
    public bool InPersonaAsociada { get; set; }
    public int Cantidad { get; set; }

    public bool PF_SelectedReadOnly => _pfSelectedReadOnly();
    public bool PFAE_SelectedReadOnly => _pfeaSelectedReadOnly();
    public bool PM_SelectedReadOnly => _pmSelectedReadOnly();



    private bool _pfSelectedReadOnly()
    {
        return PF && Required;
    }

    private bool _pfeaSelectedReadOnly()
    {
        return PFAE && Required;
    }

    private bool _pmSelectedReadOnly()
    {
        return PM && Required;
    }

}