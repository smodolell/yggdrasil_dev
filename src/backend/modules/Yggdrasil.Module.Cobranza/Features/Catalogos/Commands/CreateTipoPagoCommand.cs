using Yggdrasil.Module.Cobranza.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.Catalogos.Commands;

public record CreateTipoPagoCommand(TipoPagoEditDto Model) : ICommand<Result<int>>;

internal class CreateTipoPagoCommandHandler(
    IApplicationDbContext context,
    IValidator<TipoPagoEditDto> validator,
    IMapper mapper
) : ICommandHandler<CreateTipoPagoCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IValidator<TipoPagoEditDto> _validator = validator;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<int>> HandleAsync(CreateTipoPagoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        var tipoPago = _mapper.Map<FI_TipoPago>(model);
        _context.FI_TipoPago.Add(tipoPago);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Created(tipoPago.Id);
    }
}
