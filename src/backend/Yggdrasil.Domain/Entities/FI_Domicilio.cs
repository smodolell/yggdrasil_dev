namespace Yggdrasil.Domain.Entities;

public class FI_Domicilio
{
    public int Id { get; set; }
    [Required]
    public int PersonaId { get; set; }

    [Required]
    public int TipoDomicilioId { get; set; }

    [Required]
    public DateTime FechaRegistro { get; set; }

    //[Required]
    //public long LocalidadId { get; set; }

    [MaxLength(100)]
    public string Calle { get; set; } = "";

    [MaxLength(30)]
    public string Numero { get; set; } = "";

    [MaxLength(10)]
    public string Piso { get; set; } = "";

    [MaxLength(100)]
    public string EntreCalles { get; set; } = "";

    [MaxLength(100)]
    public string YCalle { get; set; } = "";

    [ForeignKey(nameof(PersonaId))]
    public FI_Persona FI_Persona { get; set; } = null!;


    //[ForeignKey(nameof(LocalidadId))]
    //public CAT_Localidad CAT_Localidad { get; set; } = null!;


    [ForeignKey(nameof(TipoDomicilioId))]
    public CAT_TipoDomicilio CAT_TipoDomicilio { get; set; } = null!;
}
