namespace Yggdrasil.Module.Credito.CS.Features.SelectLists;

public class GetTipoCreditoSelectListQuery : SelectListQueryBase { }

internal class GetTipoCreditoSelectListQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetTipoCreditoSelectListQuery, Result<List<SelectListItemDto>>>
{
    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetTipoCreditoSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await context.CS_TipoCredito
            .OrderBy(f => f.NomTipoCredito)
            .Select(f => new SelectListItemDto { Value = f.Id.ToString(), Text = f.CS_TipoMovimiento.Clave +" - "+ f.NomTipoCredito })
            .ToListAsync(cancellationToken);
        return Result.Success(items);
    }
}
