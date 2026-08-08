using Yggdrasil.Module.Cobranza.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.Catalogos.Queries;

public class GetTipoPagoByIdQuery : IQuery<Result<TipoPagoEditDto>>
{
    public required int TipoPagoId { get; set; }
}

internal class GetTipoPagoByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetTipoPagoByIdQuery, Result<TipoPagoEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<TipoPagoEditDto>> HandleAsync(GetTipoPagoByIdQuery message, CancellationToken cancellationToken = default)
    {
        var tipoPago = await _context.FI_TipoPago.SingleOrDefaultAsync(r => r.Id == message.TipoPagoId, cancellationToken);
        if (tipoPago == null) return Result.Error($"[NO_EXISTE][{nameof(FI_TipoPago)}]");
        var result = _mapper.Map<TipoPagoEditDto>(tipoPago);
        return Result.Success(result);
    }
}
