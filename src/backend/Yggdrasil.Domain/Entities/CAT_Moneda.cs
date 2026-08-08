namespace Yggdrasil.Domain.Entities;

public class CAT_Moneda
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string NomMoneda { get; set; } = "";

    [Required]
    [MaxLength(10)]
    public string ClaveMoneda { get; set; } = "";

    public bool PorDefecto { get; set; } = false;



}
