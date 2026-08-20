using Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.Queries;

public class GetTipoMovimientoByIdQuery : IQuery<Result<TipoMovimientoCsEditDto>>
{
    public int TipoMovimientoId { get; set; }
}

public class GetTipoMovimientoByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetTipoMovimientoByIdQuery, Result<TipoMovimientoCsEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<TipoMovimientoCsEditDto>> HandleAsync(GetTipoMovimientoByIdQuery message, CancellationToken cancellationToken = default)
    {
        var oTipoMovimiento = await _context.CS_TipoMovimiento
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == message.TipoMovimientoId, cancellationToken);
        if (oTipoMovimiento == null)
        {
            return Result.NotFound();
        }
        var dto = _mapper.Map<TipoMovimientoCsEditDto>(oTipoMovimiento);
        return Result.Success(dto);
    }
}
