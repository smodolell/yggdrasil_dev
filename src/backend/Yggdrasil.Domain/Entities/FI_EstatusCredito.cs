namespace Yggdrasil.Domain.Entities;

public class FI_EstatusCredito
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string NomEstatusCredito { get; set; } = "";

}
