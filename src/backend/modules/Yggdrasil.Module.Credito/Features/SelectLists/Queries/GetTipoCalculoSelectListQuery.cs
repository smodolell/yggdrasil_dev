namespace Yggdrasil.Module.Credito.Features.SelectLists.Queries;

public class GetTipoCalculoSelectListQuery : SelectListQueryBase
{
    public GetTipoCalculoSelectListQuery()
    {
    }
}



internal class GetTipoCalculoSelectListQueryHandler : IQueryHandler<GetTipoCalculoSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IApplicationDbContext _context;
    public GetTipoCalculoSelectListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetTipoCalculoSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.FI_TipoCalculo
            .Select(f => new SelectListItemDto
            {

                Value = f.Id.ToString(),
                Text = f.NomTipoCalculo
            }).ToListAsync(cancellationToken);

        return Result.Success(items);

    }
}