using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class CreateTasaValorCommand : ICommand<Result<int>>
{
    public int TasaId { get; set; }
    public required TasaValorDto Model { get; set; }
}

internal class CreateTasaValorCommandHandler : ICommandHandler<CreateTasaValorCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<TasaValorDto> _validator;

    public CreateTasaValorCommandHandler(IApplicationDbContext context, IValidator<TasaValorDto> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<int>> HandleAsync(CreateTasaValorCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = request.Model;

            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var tasaExiste = await _context.CAT_Tasa
                .AnyAsync(t => t.Id == request.TasaId && t.EsVariable, cancellationToken);

            if (!tasaExiste)
            {
                return Result.NotFound("Tasa variable no encontrada");
            }
            var fechaUtc = model.FecValorTasa.Date; // Normalizar la fecha (sin hora)

            var existeValorEnFecha = await _context.CAT_TasaValor
                .AnyAsync(v => v.TasaId == request.TasaId &&
                              v.Fecha == fechaUtc, cancellationToken);

            if (existeValorEnFecha)
            {
                return Result.Invalid(new ValidationError($"Ya existe un valor registrado para la fecha {fechaUtc:dd/MM/yyyy}"));
            }
            var entity = new CAT_TasaValor
            {
                TasaId = request.TasaId,
                ValorTasa = model.ValorTasa,
                Fecha = model.FecValorTasa,
                FechaRegistro = DateTime.Now
            };

            await _context.CAT_TasaValor.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Created(entity.Id);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
