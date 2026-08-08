namespace Yggdrasil.Module.Catalog.Features.SelectLists.Queries;

public class GetPeriodicidadSelectListQuery : SelectListQueryBase
{
}



internal class GetPeriodicidadSelectListQueryHandler(IApplicationDbContext context) : IQueryHandler<GetPeriodicidadSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;


    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetPeriodicidadSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.CAT_Periodicidad
            .Where(t => t.Activo)
            .Select(f => new SelectListItemDto
            {
                Value = f.Id.ToString(),
                Text = f.NomPeriodicidad
            }).ToListAsync();

        return Result.Success(items);

    }
}