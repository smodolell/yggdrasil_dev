using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Queries;

public class GetMonedaByIdQuery : IQuery<Result<MonedaEditDto>>
{
    public int MonedaId { get; set; }
}

public class GetMonedaByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetMonedaByIdQuery, Result<MonedaEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<MonedaEditDto>> HandleAsync(GetMonedaByIdQuery message, CancellationToken cancellationToken = default)
    {
        var oMoneda = await _context.CAT_Moneda.SingleOrDefaultAsync(r => r.Id == message.MonedaId, cancellationToken);
        if (oMoneda == null)
        {
            return Result.NotFound();
        }
        var monedaDto = _mapper.Map<MonedaEditDto>(oMoneda);
        return Result.Success(monedaDto);
    }
}
