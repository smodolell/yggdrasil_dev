using Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.Intradias.Commands;

public record PagoInteresCommand(PagoInteresDto Model) : ICommand<Result>;

internal class PagoInteresCommandHandler(IApplicationDbContext context, ICommandMediator mediator)
    : ICommandHandler<PagoInteresCommand, Result>
{
    public async Task<Result> HandleAsync(PagoInteresCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var fechaPago = model.FechaPago.Date;

        if (model.Monto <= 0)
            return Result.Invalid(new ValidationError("El monto a abonar debe ser mayor a cero."));

        var credito = await context.DEV_CreditoIntraDia
            .SingleOrDefaultAsync(c => c.Id == model.CreditoId, cancellationToken);

        if (credito == null)
            return Result.NotFound("Crédito no encontrado");

        var interesAcumulado = await context.DEV_InteresAcumulado
            .SingleOrDefaultAsync(i => i.CreditoId == credito.Id, cancellationToken);

        // -----------------------------------------------------------------------------------------
        // 1. VALIDACIÓN DE FECHA Y CORTE DE DEVENGO A LA FECHA DE PAGO
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

            if (fechaPago > ultimaFechaDevengada)
            {
                var resultDevengar = await mediator.SendAsync(new DevengarCommand(credito.Id, fechaPago), cancellationToken);
                if (!resultDevengar.IsSuccess)
                    return Result.Error("No se pudo devengar los intereses pendientes a la fecha de pago.");

                interesAcumulado = await context.DEV_InteresAcumulado
                    .SingleOrDefaultAsync(i => i.CreditoId == credito.Id, cancellationToken);
            }
        }
        else
        {
            if (fechaPago < credito.FechaPrimeraRenta.Date)
            {
                return Result.Invalid(new ValidationError(
                    $"La fecha de pago ({fechaPago:dd/MM/yyyy}) no puede ser anterior " +
                    $"a la fecha de origen del crédito ({credito.FechaPrimeraRenta:dd/MM/yyyy})."));
            }

            var resultDevengar = await mediator.SendAsync(new DevengarCommand(credito.Id, fechaPago), cancellationToken);
            if (!resultDevengar.IsSuccess)
                return Result.Error("No se pudo devengar la tasa inicial.");

            interesAcumulado = await context.DEV_InteresAcumulado
                .SingleOrDefaultAsync(i => i.CreditoId == credito.Id, cancellationToken);
        }

        if (interesAcumulado == null)
            return Result.Error("Error al sincronizar el estado del devengamiento.");

        // -----------------------------------------------------------------------------------------
        // 2. VALIDACIÓN DE COBRANZA Y CÁLCULO DE APLICACIÓN (CASCADA IVA -> INTERÉS)
        // -----------------------------------------------------------------------------------------
        var totalAdeudadoEnBolsa = interesAcumulado.Interes + interesAcumulado.Iva;

        if (totalAdeudadoEnBolsa <= 0)
            return Result.Invalid(new ValidationError("El crédito no registra intereses ni IVA adeudados a la fecha."));

        if (model.Monto > totalAdeudadoEnBolsa)
            return Result.Invalid(new ValidationError(
                $"El monto ingresado ({model.Monto:C}) supera el total de intereses e IVA acumulados ({totalAdeudadoEnBolsa:C})."));

        // Desglose del pago recibido abonando con preferencia al IVA y luego al Interés
        decimal montoRemanente = model.Monto;

        // A) Descontar de la bolsa de IVA
        decimal pagoIva = Math.Min(montoRemanente, interesAcumulado.Iva);
        interesAcumulado.Iva -= pagoIva;
        montoRemanente -= pagoIva;

        // B) Descontar de la bolsa de Interés
        decimal pagoInteres = Math.Min(montoRemanente, interesAcumulado.Interes);
        interesAcumulado.Interes -= pagoInteres;

        context.DEV_InteresAcumulado.Update(interesAcumulado);

        // -----------------------------------------------------------------------------------------
        // 3. REGISTRO DEL MOVIMIENTO HISTÓRICO
        // -----------------------------------------------------------------------------------------
        var nro = (await context.DEV_MovimientoIntraDia
            .Where(m => m.CreditoId == credito.Id)
            .Select(m => (int?)m.Nro)
            .MaxAsync(cancellationToken) ?? 0) + 1;

        var movimiento = new DEV_MovimientoIntraDia
        {
            CreditoId = credito.Id,
            Nro = nro,
            Concepto = "Pago de Intereses e IVA",
            Fecha = fechaPago,
            Capital = 0m,
            Interes = -pagoInteres,
            Iva = -pagoIva,
            FechaRegistro = DateTime.Now,
            SaldoInsolutoResultante = interesAcumulado.SaldoInsoluto
        };

        await context.DEV_MovimientoIntraDia.AddAsync(movimiento, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
