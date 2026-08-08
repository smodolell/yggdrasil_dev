using Refit;
using Yggdrasil.Blazor.DTOs;
using Yggdrasil.Module.Layout.UI.Services.Auth.DTOs;

namespace Yggdrasil.Module.Layout.UI.Services.Auth;

public interface IAuthApi
{
    #region Auth

    [Post("/api/auth/login")]
    Task<ApiResponseDto<UsuarioLoginDto>> Login([Body] LoginCommand model);

    [Get("/api/auth/validate-token")]
    Task<ApiResponseDto<TokenValidationDto>> ValidateToken([Query] string token);

    #endregion


}
