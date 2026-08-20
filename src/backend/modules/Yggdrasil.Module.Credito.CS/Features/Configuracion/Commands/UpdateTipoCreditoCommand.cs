using Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.Commands;

public class UpdateTipoCreditoCommand : ICommand<Result>
{
    public int TipoCreditoId { get; set; }
    public required TipoCreditoCsEditDto Model { get; set; }
}

internal class UpdateTipoCreditoCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<TipoCreditoCsEditDto> validator
) : ICommandHandler<UpdateTipoCreditoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<TipoCreditoCsEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdateTipoCreditoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var oTipoCredito = await _context.CS_TipoCredito.SingleOrDefaultAsync(r => r.Id == message.TipoCreditoId, cancellationToken);
            if (oTipoCredito == null)
            {
                return Result.NotFound();
            }

            _mapper.Map(model, oTipoCredito);
            _context.CS_TipoCredito.Update(oTipoCredito);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
