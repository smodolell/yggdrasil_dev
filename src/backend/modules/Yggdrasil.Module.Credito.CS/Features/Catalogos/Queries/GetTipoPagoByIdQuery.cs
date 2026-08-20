using Yggdrasil.Module.Credito.CS.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Catalogos.Queries;

public class GetTipoPagoByIdQuery : IQuery<Result<TipoPagoCsEditDto>>
{
    public int TipoPagoId { get; set; }
}

public class GetTipoPagoByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetTipoPagoByIdQuery, Result<TipoPagoCsEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<TipoPagoCsEditDto>> HandleAsync(GetTipoPagoByIdQuery message, CancellationToken cancellationToken = default)
    {
        var oTipoPago = await _context.CS_TipoPago
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == message.TipoPagoId, cancellationToken);
        if (oTipoPago == null)
        {
            return Result.NotFound();
        }
        var dto = _mapper.Map<TipoPagoCsEditDto>(oTipoPago);
        return Result.Success(dto);
    }
}
