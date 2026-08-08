namespace Yggdrasil.Module.Credito.Features.SelectLists.Queries;

public class GetTipoPagoSelectListQuery : SelectListQueryBase { }

internal class GetTipoPagoSelectListQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetTipoPagoSelectListQuery, Result<List<SelectListItemDto>>>
{
    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetTipoPagoSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await context.FI_TipoPago
            .OrderBy(f => f.NomTipoPago)
            .Select(f => new SelectListItemDto { Value = f.Id.ToString(), Text = f.NomTipoPago })
            .ToListAsync(cancellationToken);
        return Result.Success(items);
    }
}
