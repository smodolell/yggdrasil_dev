namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

public class PlanTipoSeguroDto
{
    public int PlanId { get; set; }

    public List<PlanTipoSeguroItemDto> Items { get; set; } = new List<PlanTipoSeguroItemDto>();
}


public class PlanTipoSeguroItemDto
{
    public int TipoSeguroId { get; set; }
    public string NomTipoSeguro { get; set; } = "";
    public bool Activo { get; set; }

}

