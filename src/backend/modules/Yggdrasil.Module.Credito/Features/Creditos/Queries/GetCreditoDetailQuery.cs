using Yggdrasil.Module.Credito.Features.Creditos.DTOs;

namespace Yggdrasil.Module.Credito.Features.Creditos.Queries;

public class GetCreditoDetailQuery : IQuery<Result<CreditoDetailDto>>
{
    public int Id { get; set; }
}

internal class GetCreditoDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetCreditoDetailQuery, Result<CreditoDetailDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CreditoDetailDto>> HandleAsync(
        GetCreditoDetailQuery message,
        CancellationToken cancellationToken = default)
    {
        var credito = await _context.FI_Credito
            .Include(c => c.FI_Persona)
            .Include(c => c.FI_Producto)
            .Include(c => c.FI_EstatusCredito)
            .Include(c => c.CAT_Moneda)
            .Include(c => c.CAT_Periodicidad)
            .SingleOrDefaultAsync(c => c.Id == message.Id, cancellationToken);

        if (credito is null)
            return Result.NotFound($"[NO_EXISTE][{nameof(FI_Credito)}]");

        return Result.Success(_mapper.Map<CreditoDetailDto>(credito));
    }
}
