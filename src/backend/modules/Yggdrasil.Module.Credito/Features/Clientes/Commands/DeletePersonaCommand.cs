namespace Yggdrasil.Module.Credito.Features.Clientes.Commands;

public record DeletePersonaCommand(int Id) : ICommand<Result>;

public class DeletePersonaCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeletePersonaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeletePersonaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oPersona = await _context.FI_Persona.SingleOrDefaultAsync(r => r.Id == message.Id, cancellationToken);
            if (oPersona == null)
            {
                return Result.Error($"[NO_EXISTE][{nameof(FI_Persona)}]");
            }

            _context.FI_Persona.Remove(oPersona);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.SuccessWithMessage("Eliminado");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
