namespace Yggdrasil.Module.Catalog.Features.SelectLists.Queries;

public class GetTasaIvaSelectListQuery : SelectListQueryBase
{
}



internal class GetTasaIvaSelectListQueryHandler(IApplicationDbContext context) : IQueryHandler<GetTasaIvaSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetTasaIvaSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.CAT_TasaIva
            .Where(r => r.Activo)
            .Select(f => new SelectListItemDto
            {
                Value = f.Id.ToString(),
                Text = f.NomTasaIva,
                ValueDecimal = f.ValorTasa
            }).ToListAsync();

        return Result.Success(items);

    }
}