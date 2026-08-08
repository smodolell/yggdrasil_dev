namespace Yggdrasil.Module.Credito.Features.Clientes.Commands;

public record DeleteTelefonoCommand(int Id) : ICommand<Result>;
public class DeleteTelefonoCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteTelefonoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteTelefonoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oTelefono = await _context.FI_Telefono.SingleOrDefaultAsync(r => r.Id == message.Id, cancellationToken);
            if (oTelefono == null)
            {
                return Result.Error($"[NO_EXISTE][{nameof(FI_Telefono)}]");
            }

            _context.FI_Telefono.Remove(oTelefono);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.SuccessWithMessage("Eliminado");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
