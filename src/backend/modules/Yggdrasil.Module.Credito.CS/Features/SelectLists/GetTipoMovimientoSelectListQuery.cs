namespace Yggdrasil.Module.Credito.CS.Features.SelectLists;

public class GetTipoMovimientoSelectListQuery : SelectListQueryBase { }

internal class GetTipoMovimientoSelectListQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetTipoMovimientoSelectListQuery, Result<List<SelectListItemDto>>>
{
    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetTipoMovimientoSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await context.CS_TipoMovimiento
            .Where(f => f.Activo)
            .OrderBy(f => f.NomTipoMovimiento)
            .Select(f => new SelectListItemDto { Value = f.Id.ToString(), Text = f.Clave + " - " + f.NomTipoMovimiento })
            .ToListAsync(cancellationToken);
        return Result.Success(items);
    }
}
