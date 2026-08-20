using Yggdrasil.Module.Credito.CS.Features.Creditos.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Creditos.Queries;

/// <param name="CreditoId"></param>
/// <param name="VersionTabla"> Versión a consultar. Si es null se retorna la versión más reciente. </param>
public record GetTablaAmortizaQuery(int CreditoId, int? VersionTabla) : IQuery<Result<List<TablaAmortizaCsItemDto>>>;

internal class GetTablaAmortizaQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetTablaAmortizaQuery, Result<List<TablaAmortizaCsItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<TablaAmortizaCsItemDto>>> HandleAsync(
        GetTablaAmortizaQuery message,
        CancellationToken cancellationToken = default)
    {
        var creditoExiste = await _context.CS_Credito
            .AnyAsync(c => c.Id == message.CreditoId, cancellationToken);

        if (!creditoExiste)
            return Result.NotFound($"[NO_EXISTE][{nameof(CS_Credito)}]");

        var baseQuery = _context.CS_TablaAmortiza
            .Include(t => t.CS_TipoMovimiento)
            .Where(t => t.CreditoId == message.CreditoId);

        int version = message.VersionTabla
            ?? await baseQuery.MaxAsync(t => (int?)t.VersionTabla, cancellationToken) ?? 1;

        var filas = await baseQuery
            .Where(t => t.VersionTabla == version)
            .OrderBy(t => t.NoPago)
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<TablaAmortizaCsItemDto>>(filas);
        return Result.Success(result);
    }
}
