using Yggdrasil.Module.Cobranza.Features.CajaManual.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.CajaManual.Queries;

public record GetCajaManualQuery(int? PersonaId, int? CreditoId) : IQuery<Result<CajaManualDto>>;

internal class GetCajaManualQueryHandler(IApplicationDbContext context, IMapper mapper) : IQueryHandler<GetCajaManualQuery, Result<CajaManualDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CajaManualDto>> HandleAsync(GetCajaManualQuery message, CancellationToken cancellationToken = default)
    {
        var personaId = message.PersonaId;
        var creditoId = message.CreditoId;
        if (personaId == null && creditoId == null)
            return Result.Invalid(new ValidationError("Se requiere al menos un filtro: PersonaId o CreditoId"));

        var oPersona = await _context.FI_Persona.SingleOrDefaultAsync(r => r.Id == personaId, cancellationToken);
        var oCredito = await _context.FI_Credito
            .Include(i => i.FI_Persona)
            .SingleOrDefaultAsync(r => r.Id == creditoId, cancellationToken);

        var result = new CajaManualDto
        {
            FechaPago = DateTime.Now,
            FechaMinima = DateTime.Now.AddDays(-1),
        };

        if (oCredito != null)
        {
            result.CreditoId = oCredito.Id;
            result.PersonaId = oCredito.PersonaId;

            var data = await _context.FI_Movimiento
                .Include(i => i.FI_Credito)
                .Where(r => r.CreditoId == oCredito.Id && r.SaldoTotal > 0)
                .ToListAsync(cancellationToken);

            result.Items = _mapper.Map<List<CajaManualItemDto>>(data);

            return Result.Success(result);
        }
        else if (oPersona != null)
        {
            result.PersonaId = oPersona.Id;

            var data = await _context.FI_Movimiento
                .Include(i => i.FI_Credito)
                .Where(r => r.FI_Credito.PersonaId == oPersona.Id && r.SaldoTotal > 0)
                .ToListAsync(cancellationToken);

            result.Items = _mapper.Map<List<CajaManualItemDto>>(data);
            return Result.Success(result);
        }
        else
        {
            return Result.Error("No se pudo encontrar el credito o la persona");
        }
    }
}
