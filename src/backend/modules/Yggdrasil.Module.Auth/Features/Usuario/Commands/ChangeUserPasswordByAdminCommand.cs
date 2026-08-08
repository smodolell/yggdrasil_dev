using Microsoft.AspNetCore.Identity;
using System.Text;
using Yggdrasil.Module.Auth.Constants;
using Yggdrasil.Module.Auth.Features.Usuario.DTOs;

namespace Yggdrasil.Module.Auth.Features.Usuario.Commands;

public class ChangeUserPasswordByAdminCommand : ICommand<Result>
{
    public required UserChangePasswordDto Model { get; set; }
}

public class ChangeUserPasswordByAdminCommandHandler(
    UserManager<SYS_Usuario> userManager,
    IValidator<UserChangePasswordDto> validator
) : ICommandHandler<ChangeUserPasswordByAdminCommand, Result>
{
    private readonly UserManager<SYS_Usuario> _userManager = userManager;
    private readonly IValidator<UserChangePasswordDto> _validator = validator;

    public async Task<Result> HandleAsync(ChangeUserPasswordByAdminCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
            {
                return Result.NotFound(ResponseMessages.UserNotFound);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
            {
                return Result.SuccessWithMessage("Contraseña actualizada exitosamente.");
            }

            var sb = new StringBuilder();
            foreach (var error in result.Errors)
            {
                sb.AppendLine($"{error.Code} - {error.Description}");
            }
            return Result.Error(sb.ToString());
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
