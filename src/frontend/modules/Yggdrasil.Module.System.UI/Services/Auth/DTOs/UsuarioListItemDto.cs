namespace Yggdrasil.Module.System.UI.Services.Auth.DTOs;

public class UsuarioListItemDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
    public string NombreCompleto { get; set; } = "";
    public string Telefono { get; set; } = "";
}
