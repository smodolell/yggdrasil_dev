using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Commands;

public record CreateCuentaBancariaCommand(int PersonaId, CuentaBancariaEditDto Model) : ICommand<Result>;

public class CreateCuentaBancariaCommandHandler(
    IApplicationDbContext context,
    IMapper mapper
) : ICommandHandler<CreateCuentaBancariaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> HandleAsync(CreateCuentaBancariaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var oPersona = await _context.FI_Persona.SingleOrDefaultAsync(r => r.Id == message.PersonaId, cancellationToken);
            if (oPersona == null) return Result.Error($"[NO_EXISTE][{nameof(FI_Persona)}]");

            var oCuentaBancaria = new FI_PersonaCuentaBancaria
            {
                PersonaId = oPersona.Id,
            };

            _context.FI_PersonaCuentaBancaria.Add(oCuentaBancaria);
            _mapper.Map(model, oCuentaBancaria);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.SuccessWithMessage("Los datos se guardaron correctamente");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}