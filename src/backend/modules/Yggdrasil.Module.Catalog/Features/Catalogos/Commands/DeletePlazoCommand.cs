namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class DeletePlazoCommand : ICommand<Result>
{
    public required int PlazoId { get; set; }
}

public class DeletePlazoCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeletePlazoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeletePlazoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oPlazo = await _context.CAT_Plazo.SingleOrDefaultAsync(r => r.Id == message.PlazoId, cancellationToken);
            if (oPlazo == null)
            {
                return Result.NotFound("No se encontró el plazo.");
            }
            _context.CAT_Plazo.Remove(oPlazo);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
