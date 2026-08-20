using Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.Intradias.Commands;

public record NewDisposicionCommand(NewDisposicionDto Model) : ICommand<Result>;

internal class NewDisposicionCommandHandler(IApplicationDbContext context)
    : ICommandHandler<NewDisposicionCommand, Result>
{
    public async Task<Result> HandleAsync(NewDisposicionCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;

        if (model.Monto <= 0)
            return Result.Invalid(new ValidationError("El monto de la disposición debe ser mayor a cero."));

        var credito = await context.DEV_CreditoIntraDia
            .SingleOrDefaultAsync(c => c.Id == model.CreditoId, cancellationToken);

        if (credito == null)
            return Result.NotFound("Crédito no encontrado");

        var interesAcumulado = await context.DEV_InteresAcumulado
            .SingleOrDefaultAsync(i => i.CreditoId == credito.Id, cancellationToken);

        var fechaDisposicion = model.FechaDisposicion.Date;

        // -----------------------------------------------------------------------------------------
        // VALIDACIÓN DE FECHA RETROACTIVA CONTRA EL ÚLTIMO DEVENGAMIENTO
        // -----------------------------------------------------------------------------------------
        if (interesAcumulado != null)
        {
            var ultimaFechaDevengada = interesAcumulado.FechaCalculo.Date;

            // No se permiten disposiciones en fechas que ya fueron devengadas y cerradas por el Job
            if (fechaDisposicion <= ultimaFechaDevengada)
            {
                return Result.Invalid(new ValidationError(
                    $"La fecha de disposición ({fechaDisposicion:dd/MM/yyyy}) no puede ser anterior o igual " +
                    $"al último devengamiento registrado ({ultimaFechaDevengada:dd/MM/yyyy})."));
            }
        }
        else
        {
            // Si aún no hay devengamientos, la disposición no puede ser anterior a la apertura del crédito
            if (fechaDisposicion < credito.FechaPrimeraRenta.Date)
            {
                return Result.Invalid(new ValidationError(
                    $"La fecha de disposición ({fechaDisposicion:dd/MM/yyyy}) no puede ser anterior " +
                    $"a la fecha de origen/apertura del crédito ({credito.FechaPrimeraRenta:dd/MM/yyyy})."));
            }
        }

        // 1. Incrementar el Capital Maestro del Contrato
        credito.Capital += model.Monto;

        // 2. Sincronizar la Base de Cálculo en la Bolsa de Devengo
        if (interesAcumulado != null)
        {
            interesAcumulado.SaldoCapital = credito.Capital;
            context.DEV_InteresAcumulado.Update(interesAcumulado);
        }

        // 3. Registrar el Movimiento Histórico de Disposición
        var nro = (await context.DEV_MovimientoIntraDia
            .Where(m => m.CreditoId == credito.Id)
            .Select(m => (int?)m.Nro)
            .MaxAsync(cancellationToken) ?? 0) + 1;

        var movimiento = new DEV_MovimientoIntraDia
        {
            CreditoId = credito.Id,
            Nro = nro,
            Concepto = "Disposición de línea de crédito",
            Fecha = fechaDisposicion,
            Capital = model.Monto,
            Interes = 0m,
            Iva = 0m,
            FechaRegistro = DateTime.Now,
            SaldoInsolutoResultante = interesAcumulado!.SaldoInsoluto
        };

        await context.DEV_MovimientoIntraDia.AddAsync(movimiento, cancellationToken);

        // 4. Persistir la transacción atómica
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}