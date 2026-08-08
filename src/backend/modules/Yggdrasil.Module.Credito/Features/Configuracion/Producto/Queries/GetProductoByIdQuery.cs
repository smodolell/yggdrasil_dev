using Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Queries;

public record GetProductoByIdQuery(int ProductoId) : IQuery<Result<ProductoEditDto>>;

internal class GetProductoByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetProductoByIdQuery, Result<ProductoEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<ProductoEditDto>> HandleAsync(GetProductoByIdQuery message, CancellationToken cancellationToken = default)
    {
        var producto = await _context.FI_Producto.SingleOrDefaultAsync(r => r.Id == message.ProductoId, cancellationToken);
        if (producto == null) return Result.Error($"[NO_EXISTE][{nameof(FI_Producto)}]");
        var result = _mapper.Map<ProductoEditDto>(producto);
        return Result.Success(result);
    }
}
