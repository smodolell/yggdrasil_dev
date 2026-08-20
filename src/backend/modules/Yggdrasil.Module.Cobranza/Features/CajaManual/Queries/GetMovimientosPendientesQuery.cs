using Yggdrasil.Module.Cobranza.Features.CajaManual.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.CajaManual.Queries;

public record GetMovimientosPendientesQuery(int CreditoId) : IQuery<Result<List<MovimientoPendienteDto>>>;

internal class GetMovimientosPendientesQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetMovimientosPendientesQuery, Result<List<MovimientoPendienteDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<MovimientoPendienteDto>>> HandleAsync(GetMovimientosPendientesQuery message, CancellationToken cancellationToken = default)
    {
        var movimientos = await _context.FI_Movimiento
            .Where(m => m.CreditoId == message.CreditoId && m.SaldoTotal > 0)
            .OrderBy(m => m.FechaVencimiento)
            .ThenBy(m => m.NoPago)
            .ToListAsync(cancellationToken);

        return Result.Success(_mapper.Map<List<MovimientoPendienteDto>>(movimientos));
    }
}
