using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class UpdatePeriodicidadCommand : ICommand<Result>
{
    public int PeriodicidadId { get; set; }
    public required PeriodicidadEditDto Model { get; set; }
}

internal class UpdatePeriodicidadCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<PeriodicidadEditDto> validator
) : ICommandHandler<UpdatePeriodicidadCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<PeriodicidadEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdatePeriodicidadCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }
            var oPeriodicidad = await _context.CAT_Periodicidad.SingleOrDefaultAsync(r => r.Id == message.PeriodicidadId, cancellationToken);
            if (oPeriodicidad == null)
            {
                return Result.NotFound();
            }

            _mapper.Map(model, oPeriodicidad);
            _context.CAT_Periodicidad.Update(oPeriodicidad);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
