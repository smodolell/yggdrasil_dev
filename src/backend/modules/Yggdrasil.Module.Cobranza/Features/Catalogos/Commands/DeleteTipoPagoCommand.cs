namespace Yggdrasil.Module.Cobranza.Features.Catalogos.Commands;

public class DeleteTipoPagoCommand : ICommand<Result>
{
    public required int TipoPagoId { get; set; }
}

internal class DeleteTipoPagoCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteTipoPagoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteTipoPagoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var tipoPago = await _context.FI_TipoPago.SingleOrDefaultAsync(r => r.Id == message.TipoPagoId, cancellationToken);
            if (tipoPago == null)
            {
                return Result.NotFound("No se encontró el Tipo de Pago.");
            }
            _context.FI_TipoPago.Remove(tipoPago);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
