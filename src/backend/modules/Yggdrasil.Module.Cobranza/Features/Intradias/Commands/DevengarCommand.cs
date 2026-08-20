namespace Yggdrasil.Module.Cobranza.Features.Intradias.Commands;

public record DevengarCommand(Guid CreditoId, DateTime FechaCalculo) : ICommand<Result>;

internal class DevengarCommandHandler(IApplicationDbContext context) : ICommandHandler<DevengarCommand, Result>
{
    public async Task<Result> HandleAsync(DevengarCommand message, CancellationToken cancellationToken = default)
    {
        var credito = await context.DEV_CreditoIntraDia
            .SingleOrDefaultAsync(c => c.Id == message.CreditoId, cancellationToken);

        if (credito == null)
            return Result.NotFound("Crédito no encontrado");

        var fechaCalculo = message.FechaCalculo.Date;

        var interesAcumulado = await context.DEV_InteresAcumulado
            .SingleOrDefaultAsync(i => i.CreditoId == credito.Id, cancellationToken);

        decimal interesDelDia = 0m;
        decimal ivaDelDia = 0m;
        int diasDevengados = 0;

        if (interesAcumulado == null)
        {
            // --- PRIMER DEVENGAMIENTO ---
            var fechaInicio = credito.FechaPrimeraRenta.Date;
            diasDevengados = (fechaCalculo - fechaInicio).Days;

            if (diasDevengados <= 0)
                return Result.Success(); // Idempotente: aún no transcurre un día completo

            // Cálculo del tramo del día
            interesDelDia = (credito.Capital * (credito.Tasa / 100m) * diasDevengados) / 360m;
            ivaDelDia = interesDelDia * (credito.TasaIva / 100m);

            interesAcumulado = new DEV_InteresAcumulado
            {
                CreditoId = credito.Id,
                FechaInicio = fechaInicio,
                FechaCalculo = fechaCalculo,
                SaldoCapital = credito.Capital,
                Tasa = credito.Tasa,
                TasaIva = credito.TasaIva,
                Dias = diasDevengados,
                Interes = interesDelDia, // Bolsa inicial
                Iva = ivaDelDia          // Bolsa inicial
            };

            await context.DEV_InteresAcumulado.AddAsync(interesAcumulado, cancellationToken);
        }
        else
        {
            // --- DEVENGAMIENTOS SUBSECUENTES ---
            var fechaCalculoAnt = interesAcumulado.FechaCalculo.Date;
            diasDevengados = (fechaCalculo - fechaCalculoAnt).Days;

            if (diasDevengados <= 0)
                return Result.Success(); // Ya se devengó este día o la fecha es anterior

            // Cálculo exclusivo de los días transcurridos desde el último Job
            interesDelDia = (interesAcumulado.SaldoCapital * (interesAcumulado.Tasa / 100m) * diasDevengados) / 360m;
            ivaDelDia = interesDelDia * (interesAcumulado.TasaIva / 100m);

            // ACTUALIZACIÓN DE LA BOLSA (SUMAR AL ACUMULADO)
            interesAcumulado.FechaInicio = fechaCalculoAnt;
            interesAcumulado.FechaCalculo = fechaCalculo;
            interesAcumulado.Dias += diasDevengados; // Acumula días
            interesAcumulado.Interes += interesDelDia; // SUMA AL ACUMULADO
            interesAcumulado.Iva += ivaDelDia;         // SUMA AL ACUMULADO

            context.DEV_InteresAcumulado.Update(interesAcumulado);
        }

        if (interesDelDia <= 0)
            return Result.Success();

        // --- REGISTRO DEL MOVIMIENTO DIARIO (EL DELTA) ---
        var nro = (await context.DEV_MovimientoIntraDia
            .Where(m => m.CreditoId == credito.Id)
            .Select(m => (int?)m.Nro)
            .MaxAsync(cancellationToken) ?? 0) + 1;

        var movimiento = new DEV_MovimientoIntraDia
        {
            CreditoId = credito.Id,
            Nro = nro,
            Concepto = $"Interés devengado ({diasDevengados} d/tasa {interesAcumulado.Tasa}%)",
            Fecha = fechaCalculo,
            Capital = 0,
            Interes = interesDelDia, // Registra SOLO lo generado hoy
            Iva = ivaDelDia,         // Registra SOLO el IVA generado hoy
            FechaRegistro = DateTime.Now,
            SaldoInsolutoResultante = interesAcumulado.SaldoInsoluto
        };

        await context.DEV_MovimientoIntraDia.AddAsync(movimiento, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}