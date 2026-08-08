using Microsoft.AspNetCore.Identity;
using Yggdrasil.Module.Auth.Constants;

namespace Yggdrasil.Module.Auth.Features.Rol.Commands;

public class DeleteRolCommand : ICommand<Result>
{
    public required int RolId { get; set; }
}

public class DeleteRolCommandHandler(
    RoleManager<SYS_Rol> roleManager
) : ICommandHandler<DeleteRolCommand, Result>
{
    private readonly RoleManager<SYS_Rol> _roleManager = roleManager;

    public async Task<Result> HandleAsync(DeleteRolCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oRol = await _roleManager.FindByIdAsync(message.RolId.ToString());
            if (oRol == null)
            {
                return Result.NotFound(ResponseMessages.RoleNotFound);
            }

            await _roleManager.DeleteAsync(oRol);
            return Result.SuccessWithMessage(ResponseMessages.RoleDeletedSuccessfully);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
