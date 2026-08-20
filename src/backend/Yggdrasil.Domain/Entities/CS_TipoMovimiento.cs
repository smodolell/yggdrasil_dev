namespace Yggdrasil.Domain.Entities;

public class CS_TipoMovimiento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(6)]
    public string Clave { get; set; } = "";

    [Required]
    [MaxLength(60)]
    public string NomTipoMovimiento { get; set; } = "";


    public bool Activo { get; set; } = true;
}