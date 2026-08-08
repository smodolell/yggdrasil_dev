namespace Yggdrasil.Module.Catalog.Features.SelectLists.Queries;

public class GetTasaFijaSelectListQuery : SelectListQueryBase
{
}



internal class GetTasaFijaSelectListQueryHandler(IApplicationDbContext context) : IQueryHandler<GetTasaFijaSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetTasaFijaSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.CAT_Tasa
            .Where(r => r.Activo && !r.EsVariable)
            .Select(f => new SelectListItemDto
            {
                Value = f.Id.ToString(),
                Text = f.NomTasa,
                ValueDecimal = f.ValorTasa
            }).ToListAsync();

        return Result.Success(items);

    }
}