using Yggdrasil.Module.Credito.Features.Operaciones.CapturaCredito.DTOs;

namespace Yggdrasil.Module.Credito.Features.Operaciones.CapturaCredito.Queries;

public record GetNewCreditoQuery(int PersonaId) : IQuery<Result<CreditoEditDto>>;

internal class GetNewCreditoQueryHandler(IApplicationDbContext context, IMapper mapper) : IQueryHandler<GetNewCreditoQuery, Result<CreditoEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CreditoEditDto>> HandleAsync(GetNewCreditoQuery message, CancellationToken cancellationToken = default)
    {
        var oPersona = await _context.FI_Persona.SingleOrDefaultAsync(r => r.Id == message.PersonaId);
        if (oPersona == null) return Result.NotFound($"[NO_EXISTE][{nameof(FI_Persona)}]");
        var result = _mapper.Map<CreditoEditDto>(oPersona);

        return Result.Success(result);
    }
}
