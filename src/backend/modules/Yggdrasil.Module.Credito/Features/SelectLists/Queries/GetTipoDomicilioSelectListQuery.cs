namespace Yggdrasil.Module.Credito.Features.SelectLists.Queries;

public class GetTipoDomicilioSelectListQuery : SelectListQueryBase
{
    public GetTipoDomicilioSelectListQuery()
    {
    }
}



internal class GetTipoTipoDomicilioSelectListQueryHandler : IQueryHandler<GetTipoDomicilioSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IApplicationDbContext _context;
    public GetTipoTipoDomicilioSelectListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetTipoDomicilioSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.CAT_TipoDomicilio
            .Select(f => new SelectListItemDto
            {

                Value = f.Id.ToString(),
                Text = f.NomTipoDomicilio
            }).ToListAsync();

        return Result.Success(items);

    }
}