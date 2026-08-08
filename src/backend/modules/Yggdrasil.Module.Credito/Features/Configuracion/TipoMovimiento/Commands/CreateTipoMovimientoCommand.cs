using Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.Commands;

public record CreateTipoMovimientoCommand(TipoMovimientoEditDto Model) : ICommand<Result<int>>;

internal class CreateTipoMovimientoCommandHandler(
    IApplicationDbContext context,
    IValidator<TipoMovimientoEditDto> validator,
    IMapper mapper
) : ICommandHandler<CreateTipoMovimientoCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IValidator<TipoMovimientoEditDto> _validator = validator;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<int>> HandleAsync(CreateTipoMovimientoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        var tipoMovimiento = _mapper.Map<FI_TipoMovimiento>(model);
        _context.FI_TipoMovimiento.Add(tipoMovimiento);

        await _context.SaveChangesAsync();

        return Result.Created(tipoMovimiento.Id);
    }
}