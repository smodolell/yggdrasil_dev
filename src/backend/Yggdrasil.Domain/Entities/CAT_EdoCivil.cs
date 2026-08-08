namespace Yggdrasil.Domain.Entities;

public class CAT_EdoCivil
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string NomEdoCivil { get; set; } = "";
}
