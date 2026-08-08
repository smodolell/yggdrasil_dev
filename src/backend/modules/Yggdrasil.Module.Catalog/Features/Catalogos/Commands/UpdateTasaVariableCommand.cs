using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class UpdateTasaVariableCommand : ICommand<Result>
{
    public int Id { get; set; }
    public required TasaVariableDto Model { get; set; }
}

internal class UpdateTasaVariableCommandHandler : ICommandHandler<UpdateTasaVariableCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<TasaVariableDto> _validator;

    public UpdateTasaVariableCommandHandler(IApplicationDbContext context, IValidator<TasaVariableDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result> HandleAsync(UpdateTasaVariableCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = request.Model;

            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var tasa = await _context.CAT_Tasa
                .SingleOrDefaultAsync(t => t.Id == request.Id && t.EsVariable, cancellationToken);

            if (tasa == null)
            {
                return Result.NotFound("Tasa variable no encontrada");
            }

            tasa.NomTasa = model.NomTasa;
            tasa.Activo = model.Activo;

            _context.CAT_Tasa.Update(tasa);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
