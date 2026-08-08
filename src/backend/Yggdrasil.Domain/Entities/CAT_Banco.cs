namespace Yggdrasil.Domain.Entities;

public class CAT_Banco
{

    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string NomBanco { get; set; } = "";

    [Required]
    [MaxLength(3)]
    public string CBUPrefix { get; set; } = "";

    [Required]
    [MaxLength(3)]
    public string CodigoBCRA { get; set; } = "";

}
