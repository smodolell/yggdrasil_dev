using Microsoft.AspNetCore.Identity;
using System.Text;
using Yggdrasil.Module.Auth.Constants;
using Yggdrasil.Module.Auth.Features.Rol.DTOs;

namespace Yggdrasil.Module.Auth.Features.Rol.Commands;

public class CreateRolCommand : ICommand<Result>
{
    public required RolCreateDto Model { get; set; }
}

public class CreateRolCommandHandler(
    RoleManager<SYS_Rol> roleManager,
    IValidator<RolCreateDto> validator
) : ICommandHandler<CreateRolCommand, Result>
{
    private readonly RoleManager<SYS_Rol> _roleManager = roleManager;
    private readonly IValidator<RolCreateDto> _validator = validator;

    public async Task<Result> HandleAsync(CreateRolCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var result = await _roleManager.CreateAsync(new SYS_Rol
            {
                Name = model.Name,
                Descripcion = model.Descripcion,
                IsEnabled = true,
            });

            if (result.Succeeded)
            {
                return Result.SuccessWithMessage(ResponseMessages.RoleCreatedSuccessfully);
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
