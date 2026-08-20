using Yggdrasil.Module.Cobranza.Features.Intradias.Commands;
using Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.Intradias.Queries;

public record GetNewDisposicionQuery(Guid CreditoId) : IQuery<Result<NewDisposicionDto>>;

internal class GetNewDisposicionQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetNewDisposicionQuery, Result<NewDisposicionDto>>
{
    public async Task<Result<NewDisposicionDto>> HandleAsync(GetNewDisposicionQuery message, CancellationToken cancellationToken = default)
    {
        var credito = await context.DEV_CreditoIntraDia
            .SingleOrDefaultAsync(c => c.Id == message.CreditoId, cancellationToken);

        if (credito == null)
            return Result.NotFound("Crédito no encontrado");

        var ultimoMovimiento = await context.DEV_MovimientoIntraDia
            .Where(m => m.CreditoId == credito.Id)
            .OrderByDescending(m => m.Fecha)
            .FirstOrDefaultAsync(cancellationToken);

        var fechaDisposicion = ultimoMovimiento?.Fecha ?? credito.FechaPrimeraRenta;

        var result = new NewDisposicionDto
        {
            CreditoId = credito.Id,
            FechaDisposicion = fechaDisposicion.AddDays(1)
        };

        return Result.Success(result);
    }
}
