namespace Yggdrasil.Domain.Entities;

public class FI_FormaPago
{

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string NomFormaPago { get; set; } = "";
    //Descontado, Por Pagar

}
