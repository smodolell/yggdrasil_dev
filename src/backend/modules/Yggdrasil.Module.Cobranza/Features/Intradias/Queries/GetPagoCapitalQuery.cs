using Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.Intradias.Queries;

public record GetPagoCapitalQuery(Guid CreditoId) : IQuery<Result<PagoCapitalDto>>;

internal class GetPagoCapitalQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetPagoCapitalQuery, Result<PagoCapitalDto>>
{
    public async Task<Result<PagoCapitalDto>> HandleAsync(GetPagoCapitalQuery message, CancellationToken cancellationToken = default)
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

        var result = new PagoCapitalDto
        {
            CreditoId = credito.Id,
            FechaPago = fechaPago
        };

        return Result.Success(result);
    }
}
