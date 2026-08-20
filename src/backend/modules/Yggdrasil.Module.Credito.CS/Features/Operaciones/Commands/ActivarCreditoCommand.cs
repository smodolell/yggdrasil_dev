namespace Yggdrasil.Module.Credito.CS.Features.Operaciones.Commands;

public record ActivarCreditoCommand(int IdCredito, DateTime? FechaActivacion) : ICommand<Result>;

internal class ActivarCreditoCommandHandler(IApplicationDbContext context) : ICommandHandler<ActivarCreditoCommand, Result>
{
    public async Task<Result> HandleAsync(ActivarCreditoCommand message, CancellationToken cancellationToken = default)
    {
        var credito = await context.CS_Credito.SingleOrDefaultAsync(r => r.Id == message.IdCredito);
        if (credito == null)
        {
            return Result.NotFound("Credito no encontrado");
        }

        credito.EstatusCreditoId = 2;
        credito.FechaActivacion = message.FechaActivacion ?? DateTime.Now;
        context.CS_Credito.Update(credito);

        await context.SaveChangesAsync(cancellationToken);


        return Result.SuccessWithMessage("Activado");
    }
}
