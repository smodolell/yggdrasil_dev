namespace Yggdrasil.Domain.Entities;

public class FI_Perfil
{
    public int Id { get; set; }

    [Required]
    [MaxLength(80)]
    public string NomPerfil { get; set; } = "";

    public bool Activo { get; set; }

    public ICollection<FI_PerfilSeccion> FI_PerfilSeccion { get; set; } = new HashSet<FI_PerfilSeccion>();

    public ICollection<FI_PersonaPerfil> FI_PersonaPerfil { get; set; } = new HashSet<FI_PersonaPerfil>();
}
