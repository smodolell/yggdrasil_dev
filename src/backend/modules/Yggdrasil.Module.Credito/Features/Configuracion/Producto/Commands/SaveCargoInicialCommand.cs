using Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Commands;

public record SaveCargoInicialCommand(CargoInicialEditDto Model) : ICommand<Result<int>>;

internal class SaveCargoInicialCommandHandler(
    IApplicationDbContext context,
    IValidator<CargoInicialEditDto> validator,
    IMapper mapper
) : ICommandHandler<SaveCargoInicialCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IValidator<CargoInicialEditDto> _validator = validator;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<int>> HandleAsync(SaveCargoInicialCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        var cargo = await _context.FI_Cargo.SingleOrDefaultAsync(r => r.Id == model.CargoId, cancellationToken);
        if (cargo == null)
        {
            cargo = new FI_Cargo
            {
                ProductoId = model.ProductoId,
                EsCargoInicial = true,
            };
            _context.FI_Cargo.Add(cargo);
        }
        _mapper.Map(model, cargo);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(cargo.Id);
    }
}
