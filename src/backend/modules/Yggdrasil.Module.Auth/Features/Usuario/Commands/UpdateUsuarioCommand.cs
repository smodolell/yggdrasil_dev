using Microsoft.AspNetCore.Identity;
using System.Text;
using Yggdrasil.Module.Auth.Constants;
using Yggdrasil.Module.Auth.Features.Usuario.DTOs;

namespace Yggdrasil.Module.Auth.Features.Usuario.Commands;

public class UpdateUsuarioCommand : ICommand<Result>
{
    public required UsuarioEditDto Model { get; set; }
}

internal class UpdateUsuarioCommandHandler(
    UserManager<SYS_Usuario> userManager,
    IUserValidator<SYS_Usuario> userValidator,
    IValidator<UsuarioEditDto> validator
) : ICommandHandler<UpdateUsuarioCommand, Result>
{
    private readonly UserManager<SYS_Usuario> _userManager = userManager;
    private readonly IUserValidator<SYS_Usuario> _userValidator = userValidator;
    private readonly IValidator<UsuarioEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdateUsuarioCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var oUsuario = await _userManager.FindByIdAsync(model.UsuarioId.ToString());
            if (oUsuario == null)
            {
                return Result.NotFound(ResponseMessages.UserNotFound);
            }

            oUsuario.NombreCompleto = model.NombreCompleto;
            oUsuario.Telefono = model.Telefono;
            oUsuario.Email = model.Email;

            var validUseResult = await _userValidator.ValidateAsync(_userManager, oUsuario);
            if (!validUseResult.Succeeded)
            {
                return Result.Error(_getErrorsString(validUseResult));
            }

            var result = await _userManager.UpdateAsync(oUsuario);
            if (result.Succeeded)
            {
                return Result.SuccessWithMessage(ResponseMessages.UserUpdatedSuccessfully);
            }

            return Result.Error(_getErrorsString(result));
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
