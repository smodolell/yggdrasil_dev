namespace Yggdrasil.Module.System.UI.Services.Auth.DTOs;

public record LoginCommand
{
    public string Email { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Contrasenia { get; set; } = string.Empty;
}
