namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

public class PlanDocumentacionEditDto
{
   	public int? PlanId { get; set; }
   	public int? DocumentacionId { get; set; }
   	//public int? TipoPersonaId { get; set; }
   	//public int? PerfilId { get; set; }
    public List<int> TipoPersonas { get; set; } = new List<int>();
    public List<int> Perfiles { get; set; } = new List<int>();
}
