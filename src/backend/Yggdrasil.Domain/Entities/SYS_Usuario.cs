using Microsoft.AspNetCore.Identity;

namespace Yggdrasil.Domain.Entities;


[Table("AspNetUsers")]
public class SYS_Usuario : IdentityUser<int>
{

    [Required]
    public DateTime FechaRegistro { get; set; }

    [Required]
    [MaxLength(200)]
    public string NombreCompleto { get; set; } = "";

    [Required]
    [MaxLength(50)]
    public string Telefono { get; set; } = "";

    [MaxLength(200)]
    public string Avatar { get; set; } = "";

    public bool IsEnabled { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsSpecial { get; set; }




}
