using Yggdrasil.Module.Credito.CS.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Catalogos.Commands;

public class UpdateTipoPagoCommand : ICommand<Result>
{
    public int TipoPagoId { get; set; }
    public required TipoPagoCsEditDto Model { get; set; }
}

internal class UpdateTipoPagoCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<TipoPagoCsEditDto> validator
) : ICommandHandler<UpdateTipoPagoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<TipoPagoCsEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdateTipoPagoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var oTipoPago = await _context.CS_TipoPago.SingleOrDefaultAsync(r => r.Id == message.TipoPagoId, cancellationToken);
            if (oTipoPago == null)
            {
                return Result.NotFound();
            }

            _mapper.Map(model, oTipoPago);
            _context.CS_TipoPago.Update(oTipoPago);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
