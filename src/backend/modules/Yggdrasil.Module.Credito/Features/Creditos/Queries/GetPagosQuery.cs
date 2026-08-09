using Yggdrasil.Module.Credito.Features.Creditos.DTOs;

namespace Yggdrasil.Module.Credito.Features.Creditos.Queries;

public record GetPagosQuery(int? PersonaId, int? CreditoId) : IQuery<Result<List<PagoItemDto>>>;

internal class GetPagosQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IQueryHandler<GetPagosQuery, Result<List<PagoItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<PagoItemDto>>> HandleAsync(
        GetPagosQuery message,
        CancellationToken cancellationToken = default)
    {
        if (!message.PersonaId.HasValue && !message.CreditoId.HasValue)
            return Result.Invalid(new ValidationError("Se debe especificar PersonaId o CreditoId."));

        IQueryable<int> creditoIdsQuery;

        if (message.PersonaId.HasValue)
        {
            var tieneCreditos = await _context.FI_Credito
                .AnyAsync(c => c.PersonaId == message.PersonaId.Value, cancellationToken);

            if (!tieneCreditos)
                return Result.NotFound($"[NO_EXISTE][FI_Persona_Creditos]");

            creditoIdsQuery = _context.FI_Credito
                .Where(c => c.PersonaId == message.PersonaId.Value)
                .Select(c => c.Id);
        }
        else
        {
            var creditoExiste = await _context.FI_Credito
                .AnyAsync(c => c.Id == message.CreditoId!.Value, cancellationToken);

            if (!creditoExiste)
                return Result.NotFound($"[NO_EXISTE][{nameof(FI_Credito)}]");

            creditoIdsQuery = _context.FI_Credito
                .Where(c => c.Id == message.CreditoId!.Value)
                .Select(c => c.Id);
        }

        var pagos = await _context.FI_Pago
            .Include(p => p.FI_TipoPago)
            .Include(p => p.FI_PagoMovimiento)
            .Where(p => p.FI_PagoMovimiento
                .Any(pm => creditoIdsQuery.Contains(pm.FI_Movimiento.CreditoId)))
            .OrderBy(p => p.FechaPago)
            .ThenBy(p => p.Id)
            .ToListAsync(cancellationToken);

        return Result.Success(_mapper.Map<List<PagoItemDto>>(pagos));
    }
}
