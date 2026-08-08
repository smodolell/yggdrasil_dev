//namespace Yggdrasil.Module.Credito.Features.SelectLists.Queries;

//public class GetEstadoSelectListQuery : SelectListQueryBase
//{
//}

//internal class GetEstadoSelectListQueryHandler(IApplicationDbContext context) : IQueryHandler<GetEstadoSelectListQuery, Result<List<SelectListItemDto>>>
//{
//    private readonly IApplicationDbContext _context = context;

//    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetEstadoSelectListQuery message, CancellationToken cancellationToken = default)
//    {
//        var items = await _context.CAT_Estado
//            .OrderBy(f => f.NomEstado)
//            .Select(f => new SelectListItemDto { Value = f.Id.ToString(), Text = f.NomEstado })
//            .ToListAsync(cancellationToken);

//        return Result.Success(items);
//    }
//}
