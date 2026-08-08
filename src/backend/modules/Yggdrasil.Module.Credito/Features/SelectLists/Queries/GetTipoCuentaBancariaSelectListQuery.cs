//namespace Yggdrasil.Module.Credito.Features.SelectLists.Queries;

//public class GetTipoCuentaBancariaSelectListQuery : SelectListQueryBase
//{
//    public GetTipoCuentaBancariaSelectListQuery()
//    {
//    }
//}



//internal class GetTipoTipoCuentaBancariaSelectListQueryHandler : IQueryHandler<GetTipoCuentaBancariaSelectListQuery, Result<List<SelectListItemDto>>>
//{
//    private readonly IApplicationDbContext _context;
//    public GetTipoTipoCuentaBancariaSelectListQueryHandler(IApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<Result<List<SelectListItemDto>>> HandleAsync(GetTipoCuentaBancariaSelectListQuery message, CancellationToken cancellationToken = default)
//    {
//        var items = await _context.FI_TipoCuentaBancaria
//            .Select(f => new SelectListItemDto
//            {

//                Value = f.Id.ToString(),
//                Text = f.NomTipoCuentaBancaria
//            }).ToListAsync();

//        return Result.Success(items);

//    }
//}