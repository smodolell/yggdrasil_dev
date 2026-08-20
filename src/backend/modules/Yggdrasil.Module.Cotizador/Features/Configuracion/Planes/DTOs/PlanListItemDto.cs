namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

public class PlanListItemDto
{
    public int Id { get; set; }
    public string NomProducto { get; set; } = "";
    public string NomPlan { get; set; } = "";
    public decimal ImporteMinimo { get; set; }
    public decimal ImporteMaximo { get; set; }
    public bool Activo { get; set; }
}
