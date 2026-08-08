using Yggdrasil.Module.Cobranza.Features.Catalogos.DTOs;
using Yggdrasil.Module.Cobranza.Features.Catalogos.Specifications;


namespace Yggdrasil.Module.Cobranza.Features.Catalogos.Queries;

public class GetTipoPagosQuery : IQuery<Result<List<TipoPagoListItemDto>>>
{
    public string? SearchText { get; set; }
}

internal class GetTipoPagosQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetTipoPagosQuery, Result<List<TipoPagoListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<TipoPagoListItemDto>>> HandleAsync(GetTipoPagosQuery message, CancellationToken cancellationToken = default)
    {
        var spec = new TipoPagoSpec(message.SearchText);
        var data = await _context.FI_TipoPago.WithSpecification(spec)
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<TipoPagoListItemDto>>(data);

        return Result.Success(result);
    }
}
