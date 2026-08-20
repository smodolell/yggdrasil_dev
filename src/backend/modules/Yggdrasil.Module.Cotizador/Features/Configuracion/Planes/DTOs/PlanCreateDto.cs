namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

public class PlanCreateDto
{
    public int? ProductoId { get; set; }


    public string NomPlan { get; set; } = "";

    public string? Descripcion { get; set; }

    public List<PlanTipoPersonaDto> TipoPersonas { get; set; } = new List<PlanTipoPersonaDto>();

}
