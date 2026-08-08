using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class UpdatePlazoCommand : ICommand<Result>
{
    public int PlazoId { get; set; }
    public required PlazoEditDto Model { get; set; }
}

internal class UpdatePlazoCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<PlazoEditDto> validator
) : ICommandHandler<UpdatePlazoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<PlazoEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdatePlazoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }
            var oPlazo = await _context.CAT_Plazo.SingleOrDefaultAsync(r => r.Id == message.PlazoId, cancellationToken);
            if (oPlazo == null)
            {
                return Result.NotFound();
            }

            _mapper.Map(model, oPlazo);
            _context.CAT_Plazo.Update(oPlazo);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
