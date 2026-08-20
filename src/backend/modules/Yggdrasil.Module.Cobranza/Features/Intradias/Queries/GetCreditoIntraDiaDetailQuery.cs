using Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.Intradias.Queries;

public record GetCreditoIntraDiaDetailQuery(Guid CreditoId) : IQuery<Result<CreditoIntraDiaDetailDto>>;

internal class GetCreditoIntraDiaDetailQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetCreditoIntraDiaDetailQuery, Result<CreditoIntraDiaDetailDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CreditoIntraDiaDetailDto>> HandleAsync(GetCreditoIntraDiaDetailQuery message, CancellationToken cancellationToken = default)
    {
        var credito = await _context.DEV_CreditoIntraDia
            .Include(c => c.DEV_Movimientos)
            .Include(c => c.DEV_InteresAcumulado)
            .SingleOrDefaultAsync(c => c.Id == message.CreditoId, cancellationToken);

        if (credito == null)
            return Result.Error($"[NO_EXISTE][{nameof(DEV_CreditoIntraDia)}]");

        var result = _mapper.Map<CreditoIntraDiaDetailDto>(credito);

        result.Movimientos = _mapper.Map<List<MovimientoIntraDiaDto>>(
            credito.DEV_Movimientos.OrderByDescending(m => m.Nro));

        result.InteresesAcumulados = credito.DEV_InteresAcumulado != null
            ? [_mapper.Map<InteresAcumuladoDto>(credito.DEV_InteresAcumulado)]
            : [];

        return Result.Success(result);
    }
}
