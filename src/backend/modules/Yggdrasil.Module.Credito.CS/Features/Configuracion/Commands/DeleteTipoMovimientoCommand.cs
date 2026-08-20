namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.Commands;

public class DeleteTipoMovimientoCommand : ICommand<Result>
{
    public required int TipoMovimientoId { get; set; }
}

public class DeleteTipoMovimientoCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteTipoMovimientoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteTipoMovimientoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oTipoMovimiento = await _context.CS_TipoMovimiento.SingleOrDefaultAsync(r => r.Id == message.TipoMovimientoId, cancellationToken);
            if (oTipoMovimiento == null)
            {
                return Result.NotFound("No se encontró el tipo de movimiento.");
            }
            _context.CS_TipoMovimiento.Remove(oTipoMovimiento);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
