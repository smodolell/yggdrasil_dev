namespace Yggdrasil.Module.Credito.Features.Configuracion.Perfil.DTOs;

public class PerfilListItemDto
{
    public int Id { get; set; }
    public string NomPerfil { get; set; } = string.Empty;
    public bool Activo { get; set; }
}
