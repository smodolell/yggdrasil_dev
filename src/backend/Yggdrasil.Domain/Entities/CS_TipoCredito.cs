namespace Yggdrasil.Domain.Entities;

public class CS_TipoCredito
{
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string ClaveTipoCredito { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string NomTipoCredito { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string Prefijo { get; set; } = string.Empty;

    [MaxLength(10)]
    public string Postfijo { get; set; } = string.Empty;

    [Required]
    [ConcurrencyCheck]
    public int Consecutivo { get; set; }

    [Required]
    public int? TipoMovimientoRentaId { get; set; }


    [ForeignKey(nameof(TipoMovimientoRentaId))]
    public CS_TipoMovimiento CS_TipoMovimiento { get; set; } = null!;
    public bool Activo { get; set; }
}
