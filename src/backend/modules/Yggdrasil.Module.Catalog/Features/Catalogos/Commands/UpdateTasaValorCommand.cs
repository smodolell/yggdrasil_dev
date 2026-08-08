using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public record UpdateTasaValorCommand(int TasaValorId, TasaValorDto Model) : ICommand<Result>;

internal class UpdateTasaValorCommandHandler : ICommandHandler<UpdateTasaValorCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<TasaValorDto> _validator;

    public UpdateTasaValorCommandHandler(IApplicationDbContext context, IValidator<TasaValorDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result> HandleAsync(UpdateTasaValorCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = request.Model;

            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var tasaValor = await _context.CAT_TasaValor
                .SingleOrDefaultAsync(tv => tv.Id == request.TasaValorId, cancellationToken);

            if (tasaValor == null)
            {
                return Result.NotFound("Valor de tasa no encontrado");
            }

            tasaValor.ValorTasa = model.ValorTasa;
            tasaValor.Fecha = model.FecValorTasa;
            tasaValor.FechaRegistro = DateTime.Now;

            _context.CAT_TasaValor.Update(tasaValor);
            await _context.SaveChangesAsync(cancellationToken);

            var ultimoValorTasa = await _context.CAT_TasaValor
                .Where(tv => tv.TasaId == tasaValor.TasaId)
                .OrderByDescending(tv => tv.Fecha) // Ordenar por fecha de valor
                    .ThenByDescending(tv => tv.FechaRegistro) // En caso de misma fecha, por registro
                .FirstOrDefaultAsync(cancellationToken);

            if (ultimoValorTasa != null)
            {
                var tasa = await _context.CAT_Tasa
                    .SingleOrDefaultAsync(r => r.Id == tasaValor.TasaId, cancellationToken);

                if (tasa != null)
                {
                    tasa.ValorTasa = ultimoValorTasa.ValorTasa;

                    _context.CAT_Tasa.Update(tasa);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }



            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
