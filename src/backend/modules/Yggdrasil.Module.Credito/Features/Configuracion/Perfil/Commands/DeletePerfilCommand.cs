namespace Yggdrasil.Module.Credito.Features.Configuracion.Perfil.Commands;

public record DeletePerfilCommand(int PerfilId) : ICommand<Result>;

internal class DeletePerfilCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeletePerfilCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(
        DeletePerfilCommand message,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var oPerfil = await _context.FI_Perfil
                .SingleOrDefaultAsync(r => r.Id == message.PerfilId, cancellationToken);

            if (oPerfil == null)
                return Result.Error($"[NO_EXISTE][{nameof(FI_Perfil)}]");

            _context.FI_Perfil.Remove(oPerfil);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.SuccessWithMessage("Eliminado");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}