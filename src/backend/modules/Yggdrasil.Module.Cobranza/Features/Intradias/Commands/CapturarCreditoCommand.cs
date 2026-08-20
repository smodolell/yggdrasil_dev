namespace Yggdrasil.Module.Cobranza.Features.Intradias.Commands;

public record CapturarCreditoCommand(Guid Id, CreditoIntradiaEditDto Model) : ICommand<Result<Guid>>;


public class CapturarCreditoCommandHandler(IApplicationDbContext context,IMapper mapper) : ICommandHandler<CapturarCreditoCommand, Result<Guid>>
{
    public async Task<Result<Guid>> HandleAsync(CapturarCreditoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var credito = await context.DEV_CreditoIntraDia.SingleOrDefaultAsync(r => r.Id == message.Id, cancellationToken);
        if (credito == null)
        {
            credito = new DEV_CreditoIntraDia { Id = message.Id == Guid.Empty ? Guid.NewGuid() : message.Id };
            await context.DEV_CreditoIntraDia.AddAsync(credito, cancellationToken);
        }
        mapper.Map(model, credito);
        credito.Capital = model.MontoOtorgado;

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

        return Result.Success(credito.Id);
    }
}
public class CreditoIntradiaEditDto
{


    public decimal MontoOtorgado { get; set; }

    public decimal Capital { get; set; }

    public decimal Tasa { get; set; }

    public decimal TasaIva { get; set; }

    public DateTime FechaPrimeraRenta { get; set; }
}
