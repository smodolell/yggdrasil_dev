using Yggdrasil.Module.Credito.Features.Operaciones.CapturaCredito.DTOs;

namespace Yggdrasil.Module.Credito.Features.Operaciones.CapturaCredito.Queries;

public record GetCreditoByIdQuery(int Id) : IQuery<Result<CreditoEditDto>>;

internal class GetCreditoByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetCreditoByIdQuery, Result<CreditoEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CreditoEditDto>> HandleAsync(GetCreditoByIdQuery message, CancellationToken cancellationToken = default)
    {
        var credito = await _context.FI_Credito
            .Include(c => c.CAT_Periodicidad)
            .SingleOrDefaultAsync(c => c.Id == message.Id, cancellationToken);

        if (credito == null)
            return Result.NotFound($"[NO_EXISTE][{nameof(FI_Credito)}]");
        var result = _mapper.Map<CreditoEditDto>(credito);
        return Result.Success(result);
    }
}
