namespace Yggdrasil.Domain.Entities;

public class CS_TipoPago
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string NomTipoPago { get; set; } = "";
}
