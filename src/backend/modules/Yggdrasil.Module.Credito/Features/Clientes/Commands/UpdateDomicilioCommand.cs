using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Commands;

public record UpdateDomicilioCommand(DomicilioEditDto Model) : ICommand<Result>;


internal class UpdateDomicilioCommandHandler(IApplicationDbContext context, IMapper mapper) : ICommandHandler<UpdateDomicilioCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> HandleAsync(UpdateDomicilioCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var oDomicilio = await _context.FI_Domicilio.SingleOrDefaultAsync(r => r.Id == model.DomicilioId);
            if (oDomicilio == null) return Result.NotFound($"[NO_EXISTE][{nameof(FI_Domicilio)}]");

            _mapper.Map(model, oDomicilio);
            _context.FI_Domicilio.Update(oDomicilio);
            await _context.SaveChangesAsync();

            return Result.SuccessWithMessage("Lo datos se guardaron correctamente");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }

    }
}

