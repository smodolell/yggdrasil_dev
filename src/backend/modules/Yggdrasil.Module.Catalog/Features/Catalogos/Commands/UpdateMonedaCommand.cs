using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class UpdateMonedaCommand : ICommand<Result>
{
    public int MonedaId { get; set; }
    public required MonedaEditDto Model { get; set; }
}

internal class UpdateMonedaCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<MonedaEditDto> validator
) : ICommandHandler<UpdateMonedaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<MonedaEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdateMonedaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }
            var oMoneda = await _context.CAT_Moneda.SingleOrDefaultAsync(r => r.Id == message.MonedaId, cancellationToken);
            if (oMoneda == null)
            {
                return Result.NotFound();
            }

            _mapper.Map(model, oMoneda);
            _context.CAT_Moneda.Update(oMoneda);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
