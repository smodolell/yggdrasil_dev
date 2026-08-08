namespace Yggdrasil.Domain.Entities;

public class CAT_TipoTelefono
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string NomTipoTelefono { get; set; } = "";
}
