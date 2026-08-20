using Yggdrasil.Module.Catalog.Features.SelectLists.Queries;

public class GetBancoSelectListQuery : SelectListQueryBase
{
}



internal class GetBancoSelectListQueryHandler(IApplicationDbContext context) : IQueryHandler<GetBancoSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;


    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetBancoSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.CAT_Banco
            .Select(f => new SelectListItemDto
            {
                Value = f.Id.ToString(),
                Text = f.NomBanco
            }).ToListAsync();

        return Result.Success(items);

    }
}