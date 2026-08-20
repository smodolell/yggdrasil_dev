using Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.Commands;

public class UpdateTipoMovimientoCommand : ICommand<Result>
{
    public int TipoMovimientoId { get; set; }
    public required TipoMovimientoCsEditDto Model { get; set; }
}

internal class UpdateTipoMovimientoCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<TipoMovimientoCsEditDto> validator
) : ICommandHandler<UpdateTipoMovimientoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<TipoMovimientoCsEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdateTipoMovimientoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var oTipoMovimiento = await _context.CS_TipoMovimiento.SingleOrDefaultAsync(r => r.Id == message.TipoMovimientoId, cancellationToken);
            if (oTipoMovimiento == null)
            {
                return Result.NotFound();
            }

            _mapper.Map(model, oTipoMovimiento);
            _context.CS_TipoMovimiento.Update(oTipoMovimiento);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
