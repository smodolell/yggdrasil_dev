namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

public class PlanDestinoFinanciamientoEditDto
{
    public int PlanId { get; set; }
    public string NomPlan { get; set; } = "";

    public List<DestinoFinanciamientoEditDto> Items { get; set; } = new List<DestinoFinanciamientoEditDto>();
}

public class DestinoFinanciamientoEditDto
{
    public int DestinoFinanciamientoId { get; set; }
    public int PlanId { get; set; }
    public string NomDestinoFinanciamiento { get; set; } = "";
    public string DescDestinoFinanciamiento { get; set; } = "";
    public bool Selected { get; set; }

}