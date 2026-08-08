namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class ChangeActiveTasaIvaCommand : ICommand<Result>
{
    public int TasaIvaId { get; set; }
    public bool Active { get; set; }
}

internal class ChangeActiveTasaIvaCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<ChangeActiveTasaIvaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(ChangeActiveTasaIvaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {

            var oTasaIva = await _context.CAT_TasaIva.SingleOrDefaultAsync(r => r.Id == message.TasaIvaId, cancellationToken);
            if (oTasaIva == null)
            {
                return Result.NotFound();
            }

            oTasaIva.Activo = message.Active;
            _context.CAT_TasaIva.Update(oTasaIva);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
