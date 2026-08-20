namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

public class PlanFaseEditDto
{
    public int PlanId { get; set; }
    public string NomPlan { get; set; } = "";

    public List<FaseEditDto> Items { get; set; } = new List<FaseEditDto>();
}

public class FaseEditDto
{
    public int FaseId { get; set; }
    public int PlanId { get; set; }
    public string NomFase{ get; set; } = "";
    public bool Required { get; set; }
    public bool Selected { get; set; }
    public bool SelectedReadOnly => Required;

}

