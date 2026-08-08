namespace Yggdrasil.Module.Credito.Features.Clientes.Commands;

public record DeleteDomicilioCommand(int Id) : ICommand<Result>;

public class DeleteDomicilioCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteDomicilioCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteDomicilioCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oDomicilio = await _context.FI_Domicilio.SingleOrDefaultAsync(r => r.Id == message.Id, cancellationToken);
            if (oDomicilio == null)
            {
                return Result.Error($"[NO_EXISTE][{nameof(FI_Domicilio)}]");
            }

            _context.FI_Domicilio.Remove(oDomicilio);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.SuccessWithMessage("Eliminado");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}