namespace Yggdrasil.Domain.Entities;

public class SYS_Consecutivo
{
    [Key]
    public string NombreTabla { get; set; } = "";

    [Required]
    public int ConsecutivoId { get; set; }

    [Required]
    public DateTime FecUltimoCambio { get; set; }

}
