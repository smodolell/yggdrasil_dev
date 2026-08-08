namespace Yggdrasil.Module.Credito.Features.SelectLists.Queries;

public class GetGeneroSelectListQuery : SelectListQueryBase
{
    public GetGeneroSelectListQuery()
    {
    }
}



internal class GetTipoGeneroSelectListQueryHandler : IQueryHandler<GetGeneroSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IApplicationDbContext _context;
    public GetTipoGeneroSelectListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetGeneroSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.CAT_Genero
            .Select(f => new SelectListItemDto
            {

                Value = f.Id.ToString(),
                Text = f.NomGenero
            }).ToListAsync();

        return Result.Success(items);

    }
}
