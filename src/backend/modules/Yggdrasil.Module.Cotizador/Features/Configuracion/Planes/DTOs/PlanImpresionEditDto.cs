namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

public class PlanImpresionEditDto
{
    public int PlanId { get; set; }
    public string NomPlan { get; set; } = "";

    public List<ImpresionEditDto> Items { get; set; } = new List<ImpresionEditDto>();
}

public class ImpresionEditDto
{
    public int ImpresionId { get; set; }
    public int PlanId { get; set; }
    public string NomImpresion { get; set; } = "";
    public bool Selected { get; set; }

}

