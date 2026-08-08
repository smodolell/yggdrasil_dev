namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class DeletePeriodicidadCommand : ICommand<Result>
{
    public required int PeriodicidadId { get; set; }
}

public class DeletePeriodicidadCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeletePeriodicidadCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeletePeriodicidadCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oPeriodicidad = await _context.CAT_Periodicidad.SingleOrDefaultAsync(r => r.Id == message.PeriodicidadId, cancellationToken);
            if (oPeriodicidad == null)
            {
                return Result.NotFound("No se encontró la periodicidad.");
            }
            _context.CAT_Periodicidad.Remove(oPeriodicidad);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
