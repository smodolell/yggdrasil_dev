namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class DeleteTasaCommand : ICommand<Result>
{
    public required int TasaId { get; set; }
}

public class DeleteTasaCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteTasaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteTasaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oTasa = await _context.CAT_Tasa
                .Include(t => t.CAT_TasaValor)
                .SingleOrDefaultAsync(r => r.Id == message.TasaId, cancellationToken);
            if (oTasa == null)
            {
                return Result.NotFound("No se encontró la tasa.");
            }
            if(oTasa.CAT_TasaValor.Count > 0)
            {
                _context.CAT_TasaValor.RemoveRange(oTasa.CAT_TasaValor);
            }

            _context.CAT_Tasa.Remove(oTasa);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
