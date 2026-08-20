using System.ComponentModel.DataAnnotations;

namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

public class PlanCargoInicialDto
{
    public int CargoId { get; set; }
    public int PlanId { get; set; }
    [Required]
    [MaxLength(80)]
    public string Concepto { get; set; } = "";

    [Required]
    public int? TipoMovimientoId { get; set; }
    [Required]
    public int? TipoCalculoId { get; set; }
    [Required]
    public int? FormaPagoId { get; set; }

    [Required]
    public decimal Monto { get; set; }

    public bool PermiteEdicion { get; set; }
    public bool Activo { get; set; }


    #region View
    public string TipoMovimiento { get; set; } = "";
    public string NomFormaPago { get; set; } = "";
    public string NomTipoCalculo { get; set; } = "";
    #endregion
}
