namespace Yggdrasil.Module.Catalog.Features.SelectLists.Queries;

public class GetTasaVariableSelectListQuery : SelectListQueryBase
{
}



internal class GetTasaVariableSelectListQueryHandler(IApplicationDbContext context) : IQueryHandler<GetTasaVariableSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;


    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetTasaVariableSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.CAT_Tasa
            .Where(t => t.EsVariable)
            .Select(f => new SelectListItemDto
            {
                Value = f.Id.ToString(),
                Text = f.NomTasa,
                ValueDecimal = f.ValorTasa
            }).ToListAsync();

        return Result.Success(items);

    }
}