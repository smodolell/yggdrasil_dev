using Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.Queries;

public record GetTipoMovimientoByIdQuery(int TipoMovimientoId) : IQuery<Result<TipoMovimientoEditDto>>;

internal class GetTipoMovimientoByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetTipoMovimientoByIdQuery, Result<TipoMovimientoEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<TipoMovimientoEditDto>> HandleAsync(
        GetTipoMovimientoByIdQuery message,
        CancellationToken cancellationToken = default)
    {
        var oTipoMovimiento = await _context.FI_TipoMovimiento
            .SingleOrDefaultAsync(r => r.Id == message.TipoMovimientoId, cancellationToken);

        if (oTipoMovimiento == null)
            return Result.NotFound($"[NO_EXISTE][{nameof(FI_TipoMovimiento)}]");

        var result = _mapper.Map<TipoMovimientoEditDto>(oTipoMovimiento);
        return Result.Success(result);
    }
}