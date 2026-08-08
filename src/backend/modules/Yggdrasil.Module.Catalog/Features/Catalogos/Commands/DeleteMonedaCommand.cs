namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class DeleteMonedaCommand : ICommand<Result>
{
    public required int MonedaId { get; set; }
}

public class DeleteMonedaCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteMonedaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteMonedaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oMoneda = await _context.CAT_Moneda.SingleOrDefaultAsync(r => r.Id == message.MonedaId, cancellationToken);
            if (oMoneda == null)
            {
                return Result.NotFound("No se encontró la moneda.");
            }
            _context.CAT_Moneda.Remove(oMoneda);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
