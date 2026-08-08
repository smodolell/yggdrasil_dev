using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class UpdateTasaFijaCommand : ICommand<Result>
{
    public int TasaId { get; set; }
    public required TasaFijaEditDto Model { get; set; }
}

internal class UpdateTasaFijaCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<TasaFijaEditDto> validator
) : ICommandHandler<UpdateTasaFijaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<TasaFijaEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdateTasaFijaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }
            var oTasa = await _context.CAT_Tasa.SingleOrDefaultAsync(r => r.Id == message.TasaId, cancellationToken);
            if (oTasa == null)
            {
                return Result.NotFound();
            }

            _mapper.Map(model, oTasa);
            _context.CAT_Tasa.Update(oTasa);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}

