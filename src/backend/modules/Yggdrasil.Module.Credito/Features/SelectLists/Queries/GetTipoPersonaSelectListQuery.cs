namespace Yggdrasil.Module.Credito.Features.SelectLists.Queries;

public class GetTipoPersonaSelectListQuery : SelectListQueryBase
{
    public GetTipoPersonaSelectListQuery()
    {
    }
}



internal class GetTipoTipoPersonaSelectListQueryHandler : IQueryHandler<GetTipoPersonaSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IApplicationDbContext _context;
    public GetTipoTipoPersonaSelectListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetTipoPersonaSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.CAT_TipoPersona
            .Select(f => new SelectListItemDto
            {

                Value = f.Id.ToString(),
                Text = f.NomTipoPersona
            }).ToListAsync();

        return Result.Success(items);

    }
}