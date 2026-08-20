using System.ComponentModel.DataAnnotations;

namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

public class PlanEditDto
{
    public int PlanId { get; set; }
    public int? ProductoId { get; set; }
    public string NomPlan { get; set; } = "";
    public string? Descripcion { get; set; }

    public decimal ImporteMinimo { get; set; }
    public decimal ImporteMaximo { get; set; }

    public bool GraciaCapital { get; set; }
    public bool GraciaInteres { get; set; }
    public decimal TasaIvaConRFC { get; set; }
    public decimal TasaIvaSinRFC { get; set; }
    public int EdadMinima { get; set; }
    public int EdadMaxima { get; set; }



    public bool PersonaFisica { get; set; }
    public bool PersonaFisicaConActividadEconomica { get; set; }
    public bool PersonaMoral { get; set; }


    public List<PlanPeriodicidadDto> Periodicidades { get; set; } = new List<PlanPeriodicidadDto>();
}