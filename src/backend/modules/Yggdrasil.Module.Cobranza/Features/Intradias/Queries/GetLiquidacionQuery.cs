using Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.Intradias.Queries;

public record GetLiquidacionQuery(Guid CreditoId) : IQuery<Result<LiquidacionDto>>;

internal class GetLiquidacionQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetLiquidacionQuery, Result<LiquidacionDto>>
{
    public async Task<Result<LiquidacionDto>> HandleAsync(GetLiquidacionQuery message, CancellationToken cancellationToken = default)
    {
        var credito = await context.DEV_CreditoIntraDia
            .SingleOrDefaultAsync(c => c.Id == message.CreditoId, cancellationToken);

        if (credito == null)
            return Result.NotFound("Crédito no encontrado");

        var ultimoMovimiento = await context.DEV_MovimientoIntraDia
            .Where(m => m.CreditoId == credito.Id)
            .OrderByDescending(m => m.Fecha)
            .FirstOrDefaultAsync(cancellationToken);

        var fechaLiquidacion = (ultimoMovimiento?.Fecha ?? credito.FechaPrimeraRenta).AddDays(1);

        var result = new LiquidacionDto
        {
            CreditoId = credito.Id,
            FechaLiquidacion = fechaLiquidacion
        };

        return Result.Success(result);
    }
}
