namespace Yggdrasil.Module.Credito.CS.Features.SelectLists;

public class GetMetodoArmotizacionSelectListQuery : SelectListQueryBase { }

internal class GetMetodoArmotizacionSelectListQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetMetodoArmotizacionSelectListQuery, Result<List<SelectListItemDto>>>
{
    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetMetodoArmotizacionSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await context.CS_MetodoArmotizacion
            .Where(r => r.Activo)
            .OrderBy(f => f.NomMetodoArmotizacion)
            .Select(f => new SelectListItemDto { Value = f.Id.ToString(), Text = f.NomMetodoArmotizacion })
            .ToListAsync(cancellationToken);
        return Result.Success(items);
    }
}
