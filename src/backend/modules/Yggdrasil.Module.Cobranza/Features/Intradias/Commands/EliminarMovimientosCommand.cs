namespace Yggdrasil.Module.Cobranza.Features.Intradias.Commands;

public record EliminarMovimientosCommand(Guid CreditoId) : ICommand<Result>;

internal class EliminarMovimientosCommandHandler(IApplicationDbContext context)
    : ICommandHandler<EliminarMovimientosCommand, Result>
{
    public async Task<Result> HandleAsync(EliminarMovimientosCommand message, CancellationToken cancellationToken = default)
    {
        var credito = await context.DEV_CreditoIntraDia
            .SingleOrDefaultAsync(c => c.Id == message.CreditoId, cancellationToken);

        if (credito == null)
            return Result.NotFound("Crédito no encontrado");

        var movimientos = await context.DEV_MovimientoIntraDia
            .Where(m => m.CreditoId == credito.Id)
            .ToListAsync(cancellationToken);

        var interesesAcumulados = await context.DEV_InteresAcumulado
            .Where(i => i.CreditoId == credito.Id)
            .ToListAsync(cancellationToken);

        context.DEV_MovimientoIntraDia.RemoveRange(movimientos);
        context.DEV_InteresAcumulado.RemoveRange(interesesAcumulados);


        credito.Capital = credito.MontoOtorgado;

        context.DEV_CreditoIntraDia.Update(credito);

        await context.SaveChangesAsync(cancellationToken);
        var movimiento = new DEV_MovimientoIntraDia
        {
            CreditoId = credito.Id,
            Nro = 0,
            Concepto = "Activación de Crédito",
            Fecha = credito.FechaPrimeraRenta,
            Capital = credito.MontoOtorgado,
            Interes = 0m,
            Iva = 0m,
            FechaRegistro = DateTime.Now,
            SaldoInsolutoResultante = credito.MontoOtorgado
        };

        await context.DEV_MovimientoIntraDia.AddAsync(movimiento, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);



        return Result.Success();
    }
}
