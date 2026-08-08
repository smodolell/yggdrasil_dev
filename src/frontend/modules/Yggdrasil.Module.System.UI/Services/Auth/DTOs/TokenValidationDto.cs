namespace Yggdrasil.Module.System.UI.Services.Auth.DTOs;

public class TokenValidationDto
{
    public bool IsValid { get; set; }
    public string? ErrorMessage { get; set; }
    public UsuarioLoginDto? User { get; set; }
}