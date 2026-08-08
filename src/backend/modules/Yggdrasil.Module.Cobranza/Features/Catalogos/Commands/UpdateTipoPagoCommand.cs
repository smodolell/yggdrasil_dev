using Yggdrasil.Module.Cobranza.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.Catalogos.Commands;

public record UpdateTipoPagoCommand(TipoPagoEditDto Model) : ICommand<Result<int>>;

internal class UpdateTipoPagoCommandHandler(
    IApplicationDbContext context,
    IValidator<TipoPagoEditDto> validator,
    IMapper mapper
) : ICommandHandler<UpdateTipoPagoCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IValidator<TipoPagoEditDto> _validator = validator;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<int>> HandleAsync(UpdateTipoPagoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        var tipoPago = await _context.FI_TipoPago.SingleOrDefaultAsync(r => r.Id == model.TipoPagoId, cancellationToken);
        if (tipoPago == null) return Result.Error($"[NO_EXISTE][{nameof(FI_TipoPago)}]");
        _mapper.Map(model, tipoPago);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(tipoPago.Id);
    }
}
