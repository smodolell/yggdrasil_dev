using Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.Intradias.Queries;

public record GetPagoInteresQuery(Guid CreditoId) : IQuery<Result<PagoInteresDto>>;

internal class GetPagoInteresQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetPagoInteresQuery, Result<PagoInteresDto>>
{
    public async Task<Result<PagoInteresDto>> HandleAsync(GetPagoInteresQuery message, CancellationToken cancellationToken = default)
    {
        var credito = await context.DEV_CreditoIntraDia
            .SingleOrDefaultAsync(c => c.Id == message.CreditoId, cancellationToken);

        if (credito == null)
            return Result.NotFound("Crédito no encontrado");

        var ultimoMovimiento = await context.DEV_MovimientoIntraDia
            .Where(m => m.CreditoId == credito.Id)
            .OrderByDescending(m => m.Fecha)
            .FirstOrDefaultAsync(cancellationToken);

        var fechaPago = (ultimoMovimiento?.Fecha ?? credito.FechaPrimeraRenta);

        var result = new PagoInteresDto
        {
            CreditoId = credito.Id,
            FechaPago = fechaPago
        };

        return Result.Success(result);
    }
}
