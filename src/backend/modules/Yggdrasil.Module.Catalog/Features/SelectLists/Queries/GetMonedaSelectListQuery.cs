namespace Yggdrasil.Module.Catalog.Features.SelectLists.Queries;

public class GetMonedaSelectListQuery : SelectListQueryBase
{
}



internal class GetMonedaSelectListQueryHandler(IApplicationDbContext context) : IQueryHandler<GetMonedaSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;


    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetMonedaSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.CAT_Moneda
            .Select(f => new SelectListItemDto
            {
                Value = f.Id.ToString(),
                Text = f.NomMoneda
            }).ToListAsync();

        return Result.Success(items);

    }
}

