namespace Yggdrasil.Module.Credito.Features.Clientes.Commands;

public class DeleteCuentaBancariaCommand : ICommand<Result>
{
    public required int CuentaBancariaId { get; set; }
}

public class DeleteCuentaBancariaCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteCuentaBancariaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteCuentaBancariaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oCuentaBancaria = await _context.FI_PersonaCuentaBancaria.SingleOrDefaultAsync(r => r.Id == message.CuentaBancariaId, cancellationToken);
            if (oCuentaBancaria == null)
            {
                return Result.Error($"[NO_EXISTE][{nameof(FI_PersonaCuentaBancaria)}]");
            }

            _context.FI_PersonaCuentaBancaria.Remove(oCuentaBancaria);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.SuccessWithMessage("Cuenta bancaria eliminada");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}