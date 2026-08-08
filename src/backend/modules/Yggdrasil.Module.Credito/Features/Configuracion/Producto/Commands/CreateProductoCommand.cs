using Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Commands;

public record CreateProductoCommand(ProductoCreateDto Model) : ICommand<Result<int>>;

internal class CreateProductoCommandHandler(
    IApplicationDbContext context,
    IValidator<ProductoCreateDto> validator,
    IMapper mapper
) : ICommandHandler<CreateProductoCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IValidator<ProductoCreateDto> _validator = validator;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<int>> HandleAsync(CreateProductoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        var producto = _mapper.Map<FI_Producto>(model);
        _context.FI_Producto.Add(producto);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Created(producto.Id);
    }
}
