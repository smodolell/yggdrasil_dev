using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Commands;

public record SaveSeccionClienteEditCommand(ClienteEditDto Model) : ICommand<Result>;


internal class SaveSeccionClienteEditCommandHandler(IApplicationDbContext context, IMapper mapper) : ICommandHandler<SaveSeccionClienteEditCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> HandleAsync(SaveSeccionClienteEditCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;

        var oPersona = await _context.FI_Persona.SingleOrDefaultAsync(r => r.Id == model.PersonaId);
        if (oPersona == null) return Result.NotFound($"[NO_EXISTE][{nameof(FI_Persona)}]");

        _mapper.Map(model, oPersona);

        try
        {
            if (model.FechaConstitucion != null)
            {
                oPersona.FechaConstitucion = model.FechaConstitucion.Value;
            }
            if (model.FechaNacimiento != null)
            {
                oPersona.FechaNacimiento = model.FechaNacimiento.Value;
            }
            await _context.SaveChangesAsync();
            return Result.SuccessWithMessage("Lo datos se guardaron correctamente");
        }
        catch (Exception ex)
        {
            return Result.CriticalError(ex.Message);
        }

    }
}