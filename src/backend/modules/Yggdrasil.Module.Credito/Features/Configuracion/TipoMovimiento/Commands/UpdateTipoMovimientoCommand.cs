using Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.Commands;

public record UpdateTipoMovimientoCommand(TipoMovimientoEditDto Model) : ICommand<Result<int>>;

internal class UpdateTipoMovimientoCommandHandler(
    IApplicationDbContext context,
    IValidator<TipoMovimientoEditDto> validator,
    IMapper mapper
) : ICommandHandler<UpdateTipoMovimientoCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IValidator<TipoMovimientoEditDto> _validator = validator;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<int>> HandleAsync(UpdateTipoMovimientoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        var tipoMovimiento = await _context.FI_TipoMovimiento.SingleOrDefaultAsync(r => r.Id == model.TipoMovimientoId);
        if (tipoMovimiento == null) return Result.Error($"[NO_EXISTE][{nameof(FI_TipoMovimiento)}]");
        _mapper.Map(model, tipoMovimiento);
        await _context.SaveChangesAsync();
        return Result.Success(tipoMovimiento.Id);
    }
}