using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public record CreateTasaVariableCommand(TasaVariableDto Model) : ICommand<Result<int>>;

internal class CreateTasaVariableCommandHandler : ICommandHandler<CreateTasaVariableCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<TasaVariableDto> _validator;

    public CreateTasaVariableCommandHandler(IApplicationDbContext context, IValidator<TasaVariableDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<int>> HandleAsync(CreateTasaVariableCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = request.Model;

            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var entity = new CAT_Tasa
            {
                NomTasa  = model.NomTasa,
                ValorTasa = 0,
                EsVariable = true,
                Activo = model.Activo
            };

            await _context.CAT_Tasa.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);


            var ultimoValorTasa = await _context.CAT_TasaValor
                .Where(tv => tv.TasaId == entity.Id)
                .OrderByDescending(tv => tv.Fecha) // Ordenar por fecha de valor
                    .ThenByDescending(tv => tv.FechaRegistro) // En caso de misma fecha, por registro
                .FirstOrDefaultAsync(cancellationToken);

            if (ultimoValorTasa != null)
            {
                var tasa = await _context.CAT_Tasa
                    .SingleOrDefaultAsync(r => r.Id== entity.Id, cancellationToken);

                if (tasa != null)
                {
                    tasa.ValorTasa = ultimoValorTasa.ValorTasa;

                    _context.CAT_Tasa.Update(tasa);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            return Result.Created(entity.Id);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
