using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Commands;

public record SavePersonaFisicaCommand(PersonaFisicaEditDto Model) : ICommand<Result>;

internal class SavePersonaFisicaCommandHandler(
    IApplicationDbContext context,
    IMapper mapper
) : ICommandHandler<SavePersonaFisicaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> HandleAsync(
        SavePersonaFisicaCommand message,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var oPersona = await _context.FI_Persona
                .SingleOrDefaultAsync(r => r.Id == model.PersonaId, cancellationToken);

            if (oPersona == null)
                return Result.Error($"[NO_EXISTE][{nameof(FI_Persona)}]");

            _mapper.Map(model, oPersona);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.SuccessWithMessage("Lo datos se guardaron correctamente");
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }
    }
}