using Yggdrasil.Module.Credito.Features.Operaciones.CapturarCredito.DTOs;

namespace Yggdrasil.Module.Credito.Features.Operaciones.CapturarCredito.Commands;

public record SaveCreditoCommand(CreditoEditDto Model) : ICommand<Result>;

public class SaveCreditoCommandHandler : ICommandHandler<SaveCreditoCommand, Result>
{
    public Task<Result> HandleAsync(SaveCreditoCommand message, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
