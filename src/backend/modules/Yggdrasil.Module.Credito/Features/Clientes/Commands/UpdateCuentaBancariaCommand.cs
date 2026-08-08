using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Commands;

public record UpdateCuentaBancariaCommand(CuentaBancariaEditDto Model) : ICommand<Result>;

internal class UpdateCuentaBancariaCommandHandler(
    IApplicationDbContext context,
    IMapper mapper
) : ICommandHandler<UpdateCuentaBancariaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> HandleAsync(UpdateCuentaBancariaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var oCuentaBancaria = await _context.FI_PersonaCuentaBancaria
                .SingleOrDefaultAsync(r => r.Id == model.CuentaBancariaId, cancellationToken);

            if (oCuentaBancaria == null)
            {
                return Result.Error($"[NO_EXISTE][{nameof(FI_PersonaCuentaBancaria)}]");
            }



            _mapper.Map(model, oCuentaBancaria);
            _context.FI_PersonaCuentaBancaria.Update(oCuentaBancaria);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.SuccessWithMessage("Lo datos se guardaron correctamente");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}