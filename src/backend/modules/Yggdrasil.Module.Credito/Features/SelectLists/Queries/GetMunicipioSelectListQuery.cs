//namespace Yggdrasil.Module.Credito.Features.SelectLists.Queries;

//public class GetMunicipioSelectListQuery : SelectListQueryBase
//{
//    public int? EstadoId { get; set; }
//}

//internal class GetMunicipioSelectListQueryHandler(IApplicationDbContext context) : IQueryHandler<GetMunicipioSelectListQuery, Result<List<SelectListItemDto>>>
//{
//    private readonly IApplicationDbContext _context = context;

//    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetMunicipioSelectListQuery message, CancellationToken cancellationToken = default)
//    {
//        var query = _context.CAT_Municipio.AsQueryable();

//        if (message.EstadoId.HasValue)
//            query = query.Where(m => m.EstadoId == message.EstadoId.Value);

//        var items = await query
//            .OrderBy(f => f.NomMunicipio)
//            .Select(f => new SelectListItemDto { Value = f.Id.ToString(), Text = f.NomMunicipio })
//            .ToListAsync(cancellationToken);

//        return Result.Success(items);
//    }
//}
