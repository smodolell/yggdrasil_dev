using Microsoft.AspNetCore.Identity;
using Yggdrasil.Module.Auth.Constants;
using Yggdrasil.Module.Auth.Features.Usuario.DTOs;

namespace Yggdrasil.Module.Auth.Features.Usuario.Queries;

public class GetUsuarioByIdQuery : IQuery<Result<UsuarioEditDto>>
{
    public required int UsuarioId { get; set; }
}

public class GetUsuarioByIdQueryHandler(
    UserManager<SYS_Usuario> userManager,
    IMapper mapper
) : IQueryHandler<GetUsuarioByIdQuery, Result<UsuarioEditDto>>
{
    private readonly UserManager<SYS_Usuario> _userManager = userManager;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<UsuarioEditDto>> HandleAsync(GetUsuarioByIdQuery message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oUsuario = await _userManager.FindByIdAsync(message.UsuarioId.ToString());
            if (oUsuario == null)
            {
                return Result.NotFound(ResponseMessages.UserNotFound);
            }

            var result = _mapper.Map<UsuarioEditDto>(oUsuario);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
