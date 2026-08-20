using Yggdrasil.Module.Credito.CS.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Catalogos.Commands;

public class CreateTipoPagoCommand : ICommand<Result<int>>
{
    public required TipoPagoCsEditDto Model { get; set; }
}

public class CreateTipoPagoCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<TipoPagoCsEditDto> validator
) : ICommandHandler<CreateTipoPagoCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<TipoPagoCsEditDto> _validator = validator;

    public async Task<Result<int>> HandleAsync(CreateTipoPagoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            var oTipoPago = _mapper.Map<CS_TipoPago>(model);
            _context.CS_TipoPago.Add(oTipoPago);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(oTipoPago.Id);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
