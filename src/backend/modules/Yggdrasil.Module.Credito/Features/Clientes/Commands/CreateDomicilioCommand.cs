using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Commands;

public record CreateDomicilioCommand(int PersonaId, DomicilioEditDto Model) : ICommand<Result>;


public class CreateDomicilioCommandHandler(
    IApplicationDbContext context,
    IMapper mapper
) : ICommandHandler<CreateDomicilioCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> HandleAsync(CreateDomicilioCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var oPersona = await _context.FI_Persona.SingleOrDefaultAsync(r => r.Id == message.PersonaId);
            if (oPersona == null) return Result.Error($"[NO_EXISTE][{nameof(FI_Persona)}]");

            var oDomicilio = new FI_Domicilio
            {
                PersonaId = oPersona.Id,
                FechaRegistro = model.FechaRegistro ?? DateTime.Now,
            };
            _context.FI_Domicilio.Add(oDomicilio);

            _mapper.Map(model, oDomicilio);
            await _context.SaveChangesAsync();

            return Result.SuccessWithMessage("Lo datos se guardaron correctamente");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}

