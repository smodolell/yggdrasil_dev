using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Commands;

public record UpdateTelefonoCommand(TelefonoEditDto Model) : ICommand<Result>;

internal class UpdateTelefonoCommandHandler(
    IApplicationDbContext context,
    IMapper mapper
) : ICommandHandler<UpdateTelefonoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> HandleAsync(UpdateTelefonoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var oTelefono = await _context.FI_Telefono
                .SingleOrDefaultAsync(r => r.Id == model.TelefonoId, cancellationToken);

            if (oTelefono == null)
            {
                return Result.Error($"[NO_EXISTE][{nameof(FI_Telefono)}]");
            }

            _mapper.Map(model, oTelefono);
            _context.FI_Telefono.Update(oTelefono);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.SuccessWithMessage("Lo datos se guardaron correctamente");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}