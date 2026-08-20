using Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.Commands;

public class CreateTipoMovimientoCommand : ICommand<Result<int>>
{
    public required TipoMovimientoCsEditDto Model { get; set; }
}

public class CreateTipoMovimientoCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<TipoMovimientoCsEditDto> validator
) : ICommandHandler<CreateTipoMovimientoCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<TipoMovimientoCsEditDto> _validator = validator;

    public async Task<Result<int>> HandleAsync(CreateTipoMovimientoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            var oTipoMovimiento = _mapper.Map<CS_TipoMovimiento>(model);
            _context.CS_TipoMovimiento.Add(oTipoMovimiento);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(oTipoMovimiento.Id);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
