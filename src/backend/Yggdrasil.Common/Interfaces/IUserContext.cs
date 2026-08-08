using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Yggdrasil.Common.Interfaces;

public interface IUserContext
{
    Guid? UserId { get; }
    string UserName { get; }
}

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // Busca el claim "sub" o "nameidentifier" que es el estándar para el ID
    public Guid? UserId
    {
        get
        {
            var id = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(id, out var guid) ? guid : null;
        }
    }

    public string UserName => _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Sistema";
}