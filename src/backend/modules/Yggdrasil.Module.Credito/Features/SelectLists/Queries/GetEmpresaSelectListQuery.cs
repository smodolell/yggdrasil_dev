namespace Yggdrasil.Module.Credito.Features.SelectLists.Queries;

public class GetEmpresaSelectListQuery : SelectListQueryBase { }

internal class GetEmpresaSelectListQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetEmpresaSelectListQuery, Result<List<SelectListItemDto>>>
{
    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetEmpresaSelectListQuery message, CancellationToken cancellationToken = default)
    {
        var items = await context.CAT_Empresa
            .OrderBy(f => f.NomEmpresa)
            .Select(f => new SelectListItemDto { Value = f.Id.ToString(), Text = f.NomEmpresa })
            .ToListAsync(cancellationToken);
        return Result.Success(items);
    }
}
