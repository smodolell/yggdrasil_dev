namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.Commands;

public class DeleteTipoCreditoCommand : ICommand<Result>
{
    public required int TipoCreditoId { get; set; }
}

public class DeleteTipoCreditoCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteTipoCreditoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteTipoCreditoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oTipoCredito = await _context.CS_TipoCredito.SingleOrDefaultAsync(r => r.Id == message.TipoCreditoId, cancellationToken);
            if (oTipoCredito == null)
            {
                return Result.NotFound("No se encontró el tipo de crédito.");
            }
            _context.CS_TipoCredito.Remove(oTipoCredito);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
