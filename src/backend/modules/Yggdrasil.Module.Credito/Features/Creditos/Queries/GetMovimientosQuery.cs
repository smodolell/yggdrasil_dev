using Yggdrasil.Module.Credito.Features.Creditos.DTOs;

namespace Yggdrasil.Module.Credito.Features.Creditos.Queries;

public record GetMovimientosQuery(int CreditoId) : IQuery<Result<List<MovimientoItemDto>>>;

internal class GetMovimientosQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetMovimientosQuery, Result<List<MovimientoItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<MovimientoItemDto>>> HandleAsync(
        GetMovimientosQuery message,
        CancellationToken cancellationToken = default)
    {
        var creditoExiste = await _context.FI_Credito
            .AnyAsync(c => c.Id == message.CreditoId, cancellationToken);

        if (!creditoExiste)
            return Result.NotFound($"[NO_EXISTE][{nameof(FI_Credito)}]");

        var movimientos = await _context.FI_Movimiento
            .Include(m => m.FI_TipoMovimiento)
            .Where(m => m.CreditoId == message.CreditoId)
            .OrderBy(m => m.FechaRegistro)
            .ThenBy(m => m.Id)
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<MovimientoItemDto>>(movimientos);
        return Result.Success(result);
    }
}
