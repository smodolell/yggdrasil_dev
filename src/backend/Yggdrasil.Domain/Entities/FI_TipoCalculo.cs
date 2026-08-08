namespace Yggdrasil.Domain.Entities;

public class FI_TipoCalculo
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string NomTipoCalculo { get; set; } = "";

    public bool EsCargoInicial { get; set; }

    public bool EsConceptoFinanciado { get; set; }

}
