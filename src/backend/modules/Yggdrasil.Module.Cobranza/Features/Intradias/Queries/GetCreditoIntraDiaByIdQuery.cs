using Yggdrasil.Module.Cobranza.Features.Intradias.Commands;

namespace Yggdrasil.Module.Cobranza.Features.Intradias.Queries;

public record GetCreditoIntraDiaByIdQuery(Guid CreditoId) : IQuery<Result<CreditoIntradiaEditDto>>;

internal class GetCreditoIntraDiaByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetCreditoIntraDiaByIdQuery, Result<CreditoIntradiaEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CreditoIntradiaEditDto>> HandleAsync(GetCreditoIntraDiaByIdQuery message, CancellationToken cancellationToken = default)
    {
        var credito = await _context.DEV_CreditoIntraDia
            .SingleOrDefaultAsync(r => r.Id == message.CreditoId, cancellationToken);

        if (credito == null)
            return Result.Error($"[NO_EXISTE][{nameof(DEV_CreditoIntraDia)}]");

        var result = _mapper.Map<CreditoIntradiaEditDto>(credito);
        return Result.Success(result);
    }
}
