namespace Yggdrasil.Module.Credito.CS.Features.Catalogos.Commands;

public class DeleteTipoPagoCommand : ICommand<Result>
{
    public required int TipoPagoId { get; set; }
}

public class DeleteTipoPagoCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteTipoPagoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteTipoPagoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oTipoPago = await _context.CS_TipoPago.SingleOrDefaultAsync(r => r.Id == message.TipoPagoId, cancellationToken);
            if (oTipoPago == null)
            {
                return Result.NotFound("No se encontró el tipo de pago.");
            }
            _context.CS_TipoPago.Remove(oTipoPago);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
