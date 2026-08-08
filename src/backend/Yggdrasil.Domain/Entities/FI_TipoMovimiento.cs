namespace Yggdrasil.Domain.Entities;

public class FI_TipoMovimiento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(6)]
    public string Clave { get; set; } = "";

    [Required]
    [MaxLength(60)]
    public string NomTipoMovimiento { get; set; } = "";

    [Required]
    public bool GeneraIvaCapital { get; set; } = false;

    public bool GeneraIvaInteres { get; set; } = false;

    public bool GeneraMora { get; set; } = false;

    public bool EsCargoInicial { get; set; } = false;
    public bool EsConceptoFinanciado { get; set; } = false;
    public bool Activo { get; set; } = true;
}

