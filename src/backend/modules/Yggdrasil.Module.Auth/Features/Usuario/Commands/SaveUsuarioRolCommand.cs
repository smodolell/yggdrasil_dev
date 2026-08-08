using Microsoft.AspNetCore.Identity;
using System.Text;
using Yggdrasil.Module.Auth.Constants;

namespace Yggdrasil.Module.Auth.Features.Usuario.Commands;

public class SaveUsuarioRolCommand : ICommand<Result>
{
    public required int UsuarioId { get; set; }
    public required Dictionary<int, bool> Data { get; set; }
}

public class SaveUsuarioRolCommandHandler(
    UserManager<SYS_Usuario> userManager,
    RoleManager<SYS_Rol> roleManager
) : ICommandHandler<SaveUsuarioRolCommand, Result>
{
    private readonly UserManager<SYS_Usuario> _userManager = userManager;
    private readonly RoleManager<SYS_Rol> _roleManager = roleManager;

    public async Task<Result> HandleAsync(SaveUsuarioRolCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oUsuario = await _userManager.FindByIdAsync(message.UsuarioId.ToString());
            if (oUsuario == null)
            {
                return Result.NotFound(ResponseMessages.UserNotFound);
            }

            var sb = new StringBuilder();
            var error = false;
            var existingRoles = await _userManager.GetRolesAsync(oUsuario);

            foreach (var item in message.Data)
            {
                var oRol = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == item.Key, cancellationToken);
                if (oRol == null) continue;

                if (item.Value && !existingRoles.Any(a => a == oRol.Name))
                {
                    var addResult = await _userManager.AddToRoleAsync(oUsuario, oRol.Name ?? "");
                    if (!addResult.Succeeded)
                    {
                        sb.AppendLine(_getErrorsString(addResult));
                        error = true;
                    }
                }
                else if (!item.Value && existingRoles.Any(a => a == oRol.Name))
                {
                    var removeResult = await _userManager.RemoveFromRoleAsync(oUsuario, oRol.Name ?? "");
                    if (!removeResult.Succeeded)
                    {
                        sb.AppendLine(_getErrorsString(removeResult));
                        error = true;
                    }
                }
            }

            return error ? Result.Error(sb.ToString()) : Result.SuccessWithMessage(ResponseMessages.RolesSavedSuccessfully);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }

    private static string _getErrorsString(IdentityResult result)
    {
        var sb = new StringBuilder();
        foreach (var error in result.Errors)
        {
            sb.AppendLine($"{error.Code} - {error.Description}");
        }
        return sb.ToString();
    }
}
