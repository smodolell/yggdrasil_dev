using Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.Queries;

public class GetTipoCreditoByIdQuery : IQuery<Result<TipoCreditoCsEditDto>>
{
    public int TipoCreditoId { get; set; }
}

public class GetTipoCreditoByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetTipoCreditoByIdQuery, Result<TipoCreditoCsEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<TipoCreditoCsEditDto>> HandleAsync(GetTipoCreditoByIdQuery message, CancellationToken cancellationToken = default)
    {
        var oTipoCredito = await _context.CS_TipoCredito
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == message.TipoCreditoId, cancellationToken);
        if (oTipoCredito == null)
        {
            return Result.NotFound();
        }
        var dto = _mapper.Map<TipoCreditoCsEditDto>(oTipoCredito);
        return Result.Success(dto);
    }
}
