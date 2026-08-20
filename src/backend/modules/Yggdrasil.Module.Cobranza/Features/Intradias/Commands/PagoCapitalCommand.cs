using Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.Intradias.Commands;

public record PagoCapitalCommand(PagoCapitalDto Model) : ICommand<Result>;

internal class PagoCapitalCommandHandler(IApplicationDbContext context, ICommandMediator mediator)
    : ICommandHandler<PagoCapitalCommand, Result>
{
    public async Task<Result> HandleAsync(PagoCapitalCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var fechaPago = model.FechaPago.Date;

        if (model.Monto <= 0)
            return Result.Invalid(new ValidationError("El monto del pago a capital debe ser mayor a cero."));

        var credito = await context.DEV_CreditoIntraDia
            .SingleOrDefaultAsync(c => c.Id == model.CreditoId, cancellationToken);

        if (credito == null)
            return Result.NotFound("Crédito no encontrado");

        if (model.Monto > credito.Capital)
            return Result.Invalid(new ValidationError(
                $"El monto a abonar ({model.Monto:C}) supera el saldo actual de capital ({credito.Capital:C})."));

        var interesAcumulado = await context.DEV_InteresAcumulado
            .SingleOrDefaultAsync(i => i.CreditoId == credito.Id, cancellationToken);

        // -----------------------------------------------------------------------------------------
        // 1. VALIDACIÓN DE FECHAS RETROACTIVAS Y CORTE DE DEVENGO
        // -----------------------------------------------------------------------------------------
        if (interesAcumulado != null)
        {
            var ultimaFechaDevengada = interesAcumulado.FechaCalculo.Date;

            if (fechaPago < ultimaFechaDevengada)
            {
                return Result.Invalid(new ValidationError(
                    $"La fecha de pago ({fechaPago:dd/MM/yyyy}) no puede ser anterior " +
                    $"al último devengamiento registrado ({ultimaFechaDevengada:dd/MM/yyyy})."));
            }

            // Si el pago es en una fecha posterior al último devengo, devengamos los días pendientes 
            // sobre el capital actual ANTES de aplicar la reducción.
            if (fechaPago > ultimaFechaDevengada)
            {
                var resultDevengar = await mediator.SendAsync(new DevengarCommand(credito.Id, fechaPago), cancellationToken);
                if (!resultDevengar.IsSuccess)
                    return Result.Error("No se pudo devengar los intereses pendientes a la fecha de pago.");

                // Recargamos el estado actualizado de la bolsa post-devengo
                interesAcumulado = await context.DEV_InteresAcumulado
                    .SingleOrDefaultAsync(i => i.CreditoId == credito.Id, cancellationToken);
            }
        }
        else
        {
            // Si es la primera operación y la fecha es menor a la fecha inicial
            if (fechaPago < credito.FechaPrimeraRenta.Date)
            {
                return Result.Invalid(new ValidationError(
                    $"La fecha de pago ({fechaPago:dd/MM/yyyy}) no puede ser anterior " +
                    $"a la fecha de origen del crédito ({credito.FechaPrimeraRenta:dd/MM/yyyy})."));
            }

            // Corremos el primer devengo
            var resultDevengar = await mediator.SendAsync(new DevengarCommand(credito.Id, fechaPago), cancellationToken);
            if (!resultDevengar.IsSuccess)
                return Result.Error("No se pudo devengar la tasa inicial.");

            interesAcumulado = await context.DEV_InteresAcumulado
                .SingleOrDefaultAsync(i => i.CreditoId == credito.Id, cancellationToken);
        }

        if (interesAcumulado == null)
            return Result.Error("Error al sincronizar el estado del devengamiento.");

        // -----------------------------------------------------------------------------------------
        // 2. REDUCCIÓN DE CAPITAL Y ACTUALIZACIÓN DE BOLSA
        // -----------------------------------------------------------------------------------------
        credito.Capital -= model.Monto;
        interesAcumulado.SaldoCapital = credito.Capital;

        context.DEV_InteresAcumulado.Update(interesAcumulado);

        // 3. REGISTRO DEL MOVIMIENTO HISTÓRICO
        var nro = (await context.DEV_MovimientoIntraDia
            .Where(m => m.CreditoId == credito.Id)
            .Select(m => (int?)m.Nro)
            .MaxAsync(cancellationToken) ?? 0) + 1;

        var movimiento = new DEV_MovimientoIntraDia
        {
            CreditoId = credito.Id,
            Nro = nro,
            Concepto = "Pago a Capital",
            Fecha = fechaPago,
            Capital = -model.Monto,
            Interes = 0m,
            Iva = 0m,
            FechaRegistro = DateTime.Now,
            SaldoInsolutoResultante = interesAcumulado.SaldoInsoluto
        };

        await context.DEV_MovimientoIntraDia.AddAsync(movimiento, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
