namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class DeleteTasaIvaCommand : ICommand<Result>
{
    public required int TasaIvaId { get; set; }
}

public class DeleteTasaIvaCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteTasaIvaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteTasaIvaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oTasaIva = await _context.CAT_TasaIva.SingleOrDefaultAsync(r => r.Id == message.TasaIvaId, cancellationToken);
            if (oTasaIva == null)
            {
                return Result.NotFound("No se encontró la tasa de IVA.");
            }
            _context.CAT_TasaIva.Remove(oTasaIva);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
