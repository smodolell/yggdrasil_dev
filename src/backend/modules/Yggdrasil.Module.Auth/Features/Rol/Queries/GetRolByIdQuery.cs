using Microsoft.AspNetCore.Identity;
using Yggdrasil.Module.Auth.Constants;
using Yggdrasil.Module.Auth.Features.Rol.DTOs;

namespace Yggdrasil.Module.Auth.Features.Rol.Queries;

public class GetRolByIdQuery : IQuery<Result<RolUpdateDto>>
{
    public required int RolId { get; set; }
}

public class GetRolByIdQueryHandler(
    RoleManager<SYS_Rol> roleManager
) : IQueryHandler<GetRolByIdQuery, Result<RolUpdateDto>>
{
    private readonly RoleManager<SYS_Rol> _roleManager = roleManager;

    public async Task<Result<RolUpdateDto>> HandleAsync(GetRolByIdQuery message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oRol = await _roleManager.FindByIdAsync(message.RolId.ToString());
            if (oRol == null)
            {
                return Result.NotFound(ResponseMessages.RoleNotFound);
            }

            var result = new RolUpdateDto
            {
                RolId = oRol.Id,
                Name = oRol.Name ?? "",
                Descripcion = oRol.Descripcion ?? "",
            };

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
