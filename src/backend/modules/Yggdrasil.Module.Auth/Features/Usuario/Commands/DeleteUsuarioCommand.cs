using Yggdrasil.Module.Auth.Constants;

namespace Yggdrasil.Module.Auth.Features.Usuario.Commands;

public class DeleteUsuarioCommand : ICommand<Result>
{
    public required int UsuarioId { get; set; }
}

public class DeleteUsuarioCommandHandler : ICommandHandler<DeleteUsuarioCommand, Result>
{
    public Task<Result> HandleAsync(DeleteUsuarioCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            return Task.FromResult(Result.Error(ResponseMessages.NotImplemented));
        }
        catch (Exception ex)
        {
            return Task.FromResult(Result.Error(ex.Message));
        }
    }
}
