using System.ComponentModel.DataAnnotations;

namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

public class PlanAsignarTasaDto
{
    public int PlanId { get; set; }


    public int? PlazoId { get; set; } = 0;
    public int? TasaId { get; set; }

    public List<PlanPlazoTasaDto> Items { get; set; } = new List<PlanPlazoTasaDto>();

    public List<SelectListItemDto> PlazoAsignar { get; set; } = [];
    public List<SelectListItemDto> TasaAsignar { get; set; } = [];
}


public class PlanPlazoTasaDto
{

    public int PlanId { get; set; }
    public int ValorPlazo { get; set; }
    public decimal ValorTasa { get; set; }
    public bool Activo { get; set; }
}