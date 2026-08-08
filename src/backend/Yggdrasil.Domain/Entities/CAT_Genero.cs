namespace Yggdrasil.Domain.Entities;

public class CAT_Genero
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string NomGenero { get; set; } = "";
}
