namespace Yggdrasil.Domain.Entities;

public class OT_PlanPlazo
{
    public int PlanId { get; set; }
    public int PlazoId { get; set; }
    public int TasaId { get; set; }
    public int ValorPlazo { get; set; }

    [Column(TypeName = "decimal(8, 4)")]
    public decimal ValorTasa { get; set; }

    public bool Activo { get; set; }

    public OT_Plan OT_Plan { get; set; } = null!;

    public CAT_Plazo CAT_Plazo { get; set; } = null!;

    public CAT_Tasa CAT_Tasa { get; set; } = null!;
}
