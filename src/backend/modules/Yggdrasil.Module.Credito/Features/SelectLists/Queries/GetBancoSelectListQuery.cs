//namespace Yggdrasil.Module.Credito.Features.SelectLists.Queries;

//public class GetBancoSelectListQuery : SelectListQueryBase
//{
//    public GetBancoSelectListQuery()
//    {
//    }
//}



//internal class GetBancoSelectListQueryHandler : IQueryHandler<GetBancoSelectListQuery, Result<List<SelectListItemDto>>>
//{
//    private readonly IApplicationDbContext _context;
//    public GetBancoSelectListQueryHandler(IApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetBancoSelectListQuery message, CancellationToken cancellationToken = default)
//    {
//        var items = await _context.CAT_Banco
//            .Select(f => new SelectListItemDto
//            {

//                Value = f.Id.ToString(),
//                Text = f.NomBanco
//            }).ToListAsync(cancellationToken);

//        return Result.Success(items);

//    }
//}