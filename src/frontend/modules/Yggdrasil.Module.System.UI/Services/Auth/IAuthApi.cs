
using Refit;
using Yggdrasil.Blazor.DTOs;
using Yggdrasil.Module.System.UI.Services.Auth.DTOs;

namespace Yggdrasil.Module.System.UI.Services.Auth;

public interface IAuthApi
{
    #region Auth

    [Post("/auth/login")]
    Task<ApiResponseDto<UsuarioLoginDto>> Login([Body] LoginCommand model);

    [Get("/auth/validate-token")]
    Task<ApiResponseDto<TokenValidationDto>> ValidateToken([Query] string token);

    #endregion

    #region Rol

    [Get("/api/auth/rol/{id}")]
    Task<ApiResponseDto<RolUpdateDto>> GetRolById(int id);

    [Get("/api/auth/rol/")]
    Task<ApiResponseDto<PagedResultDto<RolListItemDto>>> GetRoles(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10);

    [Post("/api/auth/rol/")]
    Task<IApiResponse> CreateRol([Body] RolCreateDto model);

    [Put("/api/auth/rol/{id}")]
    Task<ApiResponseDto> UpdateRol(int id, [Body] RolUpdateDto model);

    [Delete("/api/auth/rol/{id}")]
    Task<ApiResponseDto> DeleteRol(int id);

    [Patch("/api/auth/rol/{id}/active")]
    Task<ApiResponseDto> ChangeRolActive(int id, [Query] bool isEnabled);

    [Get("/api/auth/rol/{id}/menu")]
    Task<ApiResponseDto<List<MenuTreeItemDto>>> GetMenuRol(int id);

    [Post("/api/auth/rol/{id}/menu")]
    Task<ApiResponseDto> SaveMenuRol(int id, [Body] List<MenuTreeItemDto> model);

    #endregion

    #region Usuario

    [Get("/api/auth/usuario/{id}")]
    Task<ApiResponseDto<UsuarioEditDto>> GetUsuarioById(int id);

    [Get("/api/auth/usuario/")]
    Task<ApiResponseDto<PagedResultDto<UsuarioListItemDto>>> GetUsuarios(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10);

    [Post("/api/auth/usuario/")]
    Task<IApiResponse> CreateUsuario([Body] UsuarioCreateDto model);

    [Put("/api/auth/usuario/{id}")]
    Task<ApiResponseDto> UpdateUsuario(int id, [Body] UsuarioEditDto model);

    [Delete("/api/auth/usuario/{id}")]
    Task<ApiResponseDto> DeleteUsuario(int id);

    [Get("/api/auth/usuario/{id}/roles")]
    Task<ApiResponseDto<List<UsuarioRolDto>>> GetRolesByUsuarioId(int id);

    [Post("/api/auth/usuario/{id}/roles")]
    Task<ApiResponseDto> SaveUsuarioRol(int id, [Body] Dictionary<int, bool> data);

    [Post("/api/auth/usuario/change-password")]
    Task<ApiResponseDto> ChangeUserPasswordByAdmin([Body] UserChangePasswordDto model);

    #endregion
}
