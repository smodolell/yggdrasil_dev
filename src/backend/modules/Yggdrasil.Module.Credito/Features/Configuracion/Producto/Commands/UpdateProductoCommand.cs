using Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Commands;

public record UpdateProductoCommand(ProductoEditDto Model) : ICommand<Result<int>>;

internal class UpdateProductoCommandHandler(
    IApplicationDbContext context,
    IValidator<ProductoEditDto> validator,
    IMapper mapper
) : ICommandHandler<UpdateProductoCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IValidator<ProductoEditDto> _validator = validator;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<int>> HandleAsync(UpdateProductoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        var producto = await _context.FI_Producto.SingleOrDefaultAsync(r => r.Id == model.ProductoId, cancellationToken);
        if (producto == null) return Result.Error($"[NO_EXISTE][{nameof(FI_Producto)}]");
        _mapper.Map(model, producto);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(producto.Id);
    }
}
