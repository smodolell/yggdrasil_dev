using Yggdrasil.Common.DTOs;

namespace Yggdrasil.Common.Interfaces;

public interface IJwtService
{
    string GenerateToken(UsuarioLoginDto user);
    string GenerateRefreshToken();
    bool ValidateToken(string token);
    UsuarioLoginDto? GetUserFromToken(string token);
}
