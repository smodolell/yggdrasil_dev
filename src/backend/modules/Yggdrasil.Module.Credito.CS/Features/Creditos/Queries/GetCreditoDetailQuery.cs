using Yggdrasil.Module.Credito.CS.Features.Creditos.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Creditos.Queries;

public class GetCreditoDetailQuery : IQuery<Result<CreditoCsDetailDto>>
{
    public int Id { get; set; }
}

internal class GetCreditoDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetCreditoDetailQuery, Result<CreditoCsDetailDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CreditoCsDetailDto>> HandleAsync(
        GetCreditoDetailQuery message,
        CancellationToken cancellationToken = default)
    {
        var credito = await _context.CS_Credito
            .Include(c => c.CS_TipoCredito)
            .Include(c => c.CS_EstatusCredito)
            .Include(c => c.CAT_Periodicidad)
            .Include(c => c.CS_MetodoArmotizacion)
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.Id == message.Id, cancellationToken);

        if (credito is null)
            return Result.NotFound($"[NO_EXISTE][{nameof(CS_Credito)}]");

        return Result.Success(_mapper.Map<CreditoCsDetailDto>(credito));
    }
}
