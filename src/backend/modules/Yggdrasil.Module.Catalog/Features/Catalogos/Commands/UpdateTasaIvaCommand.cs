using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class UpdateTasaIvaCommand : ICommand<Result>
{
    public int TasaIvaId { get; set; }
    public required TasaIvaEditDto Model { get; set; }
}

internal class UpdateTasaIvaCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<TasaIvaEditDto> validator
) : ICommandHandler<UpdateTasaIvaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<TasaIvaEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdateTasaIvaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }
            var oTasaIva = await _context.CAT_TasaIva.SingleOrDefaultAsync(r => r.Id == message.TasaIvaId, cancellationToken);
            if (oTasaIva == null)
            {
                return Result.NotFound();
            }

            _mapper.Map(model, oTasaIva);
            _context.CAT_TasaIva.Update(oTasaIva);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
