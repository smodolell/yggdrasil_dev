namespace Yggdrasil.Domain.Entities;

public class FI_Telefono
{
    public int Id { get; set; }
    public DateTime FechaRegistro { get; set; }
    public int TipoTelefonoId { get; set; }
    public int CompaniaTelefonicaId { get; set; }
    public int PersonaId { get; set; }

    [MaxLength(60)]
    public string Numero { get; set; } = "";

    [MaxLength(40)]
    public string Extension { get; set; } = "";

    [MaxLength(100)]
    public string InfoAdicional { get; set; } = "";

    [ForeignKey(nameof(PersonaId))]
    public FI_Persona FI_Persona { get; set; } = null!;

    [ForeignKey(nameof(TipoTelefonoId))]
    public CAT_TipoTelefono CAT_TipoTelefono { get; set; } = null!;

    [ForeignKey(nameof(CompaniaTelefonicaId))]
    public CAT_CompaniaTelefonica CAT_CompaniaTelefonica { get; set; } = null!;
}
