using Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.Intradias.Commands;

public record LiquidacionCommand(LiquidacionDto Model) : ICommand<Result>;

internal class LiquidacionCommandHandler(IApplicationDbContext context, ICommandMediator mediator) : ICommandHandler<LiquidacionCommand, Result>
{
    public async Task<Result> HandleAsync(LiquidacionCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var credito = await context.DEV_CreditoIntraDia.SingleOrDefaultAsync(c => c.Id == model.CreditoId);
        if (credito == null) return Result.NotFound("Crédito no encontrado");

        var resultDevengar = await mediator.SendAsync(new DevengarCommand(credito.Id, model.FechaLiquidacion));
        if (!resultDevengar.IsSuccess) return Result.Error("No se pudo devengar");

        var interesAcumulado = await context.DEV_InteresAcumulado
            .SingleOrDefaultAsync(i => i.CreditoId == credito.Id);
        if (interesAcumulado == null) return Result.Error("No se pudo aplicar la liquidación");

        var capitalAplicado = interesAcumulado.SaldoCapital;
        var interesAplicado = interesAcumulado.Interes;
        var ivaAplicado = interesAcumulado.Iva;

        credito.Capital -= capitalAplicado;

        interesAcumulado.SaldoCapital = 0;
        interesAcumulado.Interes = 0;
        interesAcumulado.Iva = 0;
        interesAcumulado.FechaInicio = model.FechaLiquidacion;
        interesAcumulado.FechaCalculo = model.FechaLiquidacion;
        interesAcumulado.Dias = 0;

        var nro = (await context.DEV_MovimientoIntraDia
            .Where(m => m.CreditoId == credito.Id)
            .Select(m => (int?)m.Nro)
            .MaxAsync(cancellationToken) ?? 0) + 1;

        var movimiento = new DEV_MovimientoIntraDia
        {
            CreditoId = credito.Id,
            Nro = nro,
            Concepto = "Liquidación",
            Fecha = model.FechaLiquidacion,
            Capital = capitalAplicado,
            Interes = interesAplicado,
            Iva = ivaAplicado,
            FechaRegistro = DateTime.Now,
            SaldoInsolutoResultante = interesAcumulado.SaldoInsoluto
        };
        await context.DEV_MovimientoIntraDia.AddAsync(movimiento);
        await context.SaveChangesAsync();
        return Result.Success();
    }
}
