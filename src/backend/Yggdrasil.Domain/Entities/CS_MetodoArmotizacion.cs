namespace Yggdrasil.Domain.Entities;

public class CS_MetodoArmotizacion
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(120)]
    public string NomMetodoArmotizacion { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;
}
