namespace Yggdrasil.Domain.Entities;

public class CAT_TipoDomicilio
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string NomTipoDomicilio { get; set; } = "";
}
