namespace Yggdrasil.Domain.Entities;

public class RSP_Input
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string NomInput { get; set; } = "";


}
