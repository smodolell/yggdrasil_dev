namespace Yggdrasil.Module.Credito.Features.SelectLists.Queries;

public class GetFormaPagoSelectListQuery : SelectListQueryBase
{
    public GetFormaPagoSelectListQuery()
    {
    }
}



internal class GetTipoFormaPagoSelectListQueryHandler : IQueryHandler<GetFormaPagoSelectListQuery, Result<List<SelectListItemDto>>>
{
    private readonly IApplicationDbContext _context;
    public GetTipoFormaPagoSelectListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetFormaPagoSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await _context.FI_FormaPago
            .Select(f => new SelectListItemDto
            {

                Value = f.Id.ToString(),
                Text = f.NomFormaPago
            }).ToListAsync();

        return Result.Success(items);

    }
}