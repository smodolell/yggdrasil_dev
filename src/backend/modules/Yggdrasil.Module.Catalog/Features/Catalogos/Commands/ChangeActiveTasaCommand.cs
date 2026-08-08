namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class ChangeActiveTasaCommand : ICommand<Result>
{
    public int TasaId { get; set; }
    public bool Active { get; set; }
}

internal class ChangeActiveTasaCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<ChangeActiveTasaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(ChangeActiveTasaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {

            var oTasa = await _context.CAT_Tasa.SingleOrDefaultAsync(r => r.Id == message.TasaId, cancellationToken);
            if (oTasa == null)
            {
                return Result.NotFound();
            }

            oTasa.Activo = message.Active;
            _context.CAT_Tasa.Update(oTasa);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
