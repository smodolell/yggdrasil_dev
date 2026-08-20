namespace Yggdrasil.Module.Cobranza.Features.Intradias.Commands;


public record PrepagoCommand(PrepagoDto Model) : ICommand<Result>;

internal class PrepagoCommandHandler(IApplicationDbContext context, ICommandMediator mediator)
    : ICommandHandler<PrepagoCommand, Result>
{
    public async Task<Result> HandleAsync(PrepagoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var fechaPago = model.FechaPago.Date;

        if (model.Monto <= 0)
            return Result.Invalid(new ValidationError("El monto del prepago debe ser mayor a cero."));

        var credito = await context.DEV_CreditoIntraDia
            .SingleOrDefaultAsync(c => c.Id == model.CreditoId, cancellationToken);

        if (credito == null)
            return Result.NotFound("Crédito no encontrado");

        var interesAcumulado = await context.DEV_InteresAcumulado
            .SingleOrDefaultAsync(i => i.CreditoId == credito.Id, cancellationToken);

        // -----------------------------------------------------------------------------------------
        // 1. VALIDACIÓN DE FECHAS RETROACTIVAS Y CORTE DE DEVENGO AL DÍA
        // -----------------------------------------------------------------------------------------
        if (interesAcumulado != null)
        {
            var ultimaFechaDevengada = interesAcumulado.FechaCalculo.Date;

            if (fechaPago < ultimaFechaDevengada)
            {
                return Result.Invalid(new ValidationError(
                    $"La fecha del prepago ({fechaPago:dd/MM/yyyy}) no puede ser anterior " +
                    $"al último devengamiento registrado ({ultimaFechaDevengada:dd/MM/yyyy})."));
            }

            if (fechaPago > ultimaFechaDevengada)
            {
                var resultDevengar = await mediator.SendAsync(new DevengarCommand(credito.Id, fechaPago), cancellationToken);
                if (!resultDevengar.IsSuccess)
                    return Result.Error("No se pudo devengar los intereses a la fecha del prepago.");

                interesAcumulado = await context.DEV_InteresAcumulado
                    .SingleOrDefaultAsync(i => i.CreditoId == credito.Id, cancellationToken);
            }
        }
        else
        {
            if (fechaPago < credito.FechaPrimeraRenta.Date)
            {
                return Result.Invalid(new ValidationError(
                    $"La fecha del prepago ({fechaPago:dd/MM/yyyy}) no puede ser anterior " +
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
        // 2. VALIDACIÓN DEL MONTO MÁXIMO (DEUDA TOTAL)
        // -----------------------------------------------------------------------------------------
        var deudaTotal = interesAcumulado.SaldoCapital + interesAcumulado.Interes + interesAcumulado.Iva;

        if (model.Monto > deudaTotal)
        {
            return Result.Invalid(new ValidationError(
                $"El monto ingresado ({model.Monto:C}) supera la deuda total de la línea ({deudaTotal:C})."));
        }

        // -----------------------------------------------------------------------------------------
        // 3. CASCADA DE IMPUTACIÓN (IVA -> INTERÉS -> CAPITAL)
        // -----------------------------------------------------------------------------------------
        decimal montoRestante = model.Monto;

        // A) Primero absorbe el IVA acumulado
        decimal ivaAplicado = Math.Min(montoRestante, interesAcumulado.Iva);
        montoRestante -= ivaAplicado;

        // B) Segundo absorbe el Interés acumulado
        decimal interesAplicado = Math.Min(montoRestante, interesAcumulado.Interes);
        montoRestante -= interesAplicado;

        // C) El sobrante reduce el Capital Maestro
        decimal capitalAplicado = Math.Min(montoRestante, interesAcumulado.SaldoCapital);

        // Actualización de Entidades
        credito.Capital -= capitalAplicado;
        interesAcumulado.SaldoCapital = credito.Capital;
        interesAcumulado.Interes -= interesAplicado;
        interesAcumulado.Iva -= ivaAplicado;

        context.DEV_InteresAcumulado.Update(interesAcumulado);

        // -----------------------------------------------------------------------------------------
        // 4. REGISTRO DEL MOVIMIENTO HISTÓRICO DE PREPAGO
        // -----------------------------------------------------------------------------------------
        var nro = (await context.DEV_MovimientoIntraDia
            .Where(m => m.CreditoId == credito.Id)
            .Select(m => (int?)m.Nro)
            .MaxAsync(cancellationToken) ?? 0) + 1;

        var movimiento = new DEV_MovimientoIntraDia
        {
            CreditoId = credito.Id,
            Nro = nro,
            Concepto = "Prepago",
            Fecha = fechaPago,
            Capital = -capitalAplicado,
            Interes = -interesAplicado,
            Iva = -ivaAplicado,
            FechaRegistro = DateTime.Now,
            SaldoInsolutoResultante = interesAcumulado.SaldoInsoluto
        };

        await context.DEV_MovimientoIntraDia.AddAsync(movimiento, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

public class PrepagoDto
{
    public Guid CreditoId { get; set; }

    public decimal Monto { get; set; }
    public DateTime FechaPago { get; set; }
}
