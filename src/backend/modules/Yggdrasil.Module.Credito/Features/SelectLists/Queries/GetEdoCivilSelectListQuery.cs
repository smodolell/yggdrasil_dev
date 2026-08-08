namespace Yggdrasil.Module.Credito.Features.SelectLists.Queries;

public class GetEdoCivilSelectListQuery : SelectListQueryBase
{
    public GetEdoCivilSelectListQuery()
    {
    }
}



internal class GetTipoEdoCivilSelectListQueryHandler : IQueryHandler<GetEdoCivilSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IApplicationDbContext _context;
    public GetTipoEdoCivilSelectListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetEdoCivilSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.CAT_EdoCivil
            .Select(f => new SelectListItemDto
            {

                Value = f.Id.ToString(),
                Text = f.NomEdoCivil
            }).ToListAsync();

        return Result.Success(items);

    }
}