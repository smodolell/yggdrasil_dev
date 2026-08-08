using Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Queries;

public record GetProductoDetailQuery(int ProductoId) : IQuery<Result<ProductoDetailDto>>;

internal class GetProductoDetailQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetProductoDetailQuery, Result<ProductoDetailDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<ProductoDetailDto>> HandleAsync(GetProductoDetailQuery message, CancellationToken cancellationToken = default)
    {
        var producto = await _context.FI_Producto
            .Include(p => p.CAT_Empresa)
            .Include(p => p.CAT_Moneda)
            .SingleOrDefaultAsync(r => r.Id == message.ProductoId, cancellationToken);

        if (producto == null)
            return Result.Error($"[NO_EXISTE][{nameof(FI_Producto)}]");

        var dto = new ProductoDetailDto
        {
            ProductoId = producto.Id,
            NomProducto = producto.NomProducto,
            ClaveProducto = producto.ClaveProducto,
            Posfijo = producto.Posfijo,
            Prefijo = producto.Prefijo,
            Consecutivo = producto.Consecutivo,
            Activo = producto.Activo,
            NomEmpresaOtorgante = producto.CAT_Empresa?.NomEmpresa ?? "",
            NomMoneda = producto.CAT_Moneda?.NomMoneda ?? "",
        };

        if (producto.TipoMovimientoRentaId.HasValue)
        {
            var tmRenta = await _context.FI_TipoMovimiento
                .SingleOrDefaultAsync(r => r.Id == producto.TipoMovimientoRentaId.Value, cancellationToken);
            dto.NomTipoMovimientoRenta = tmRenta?.NomTipoMovimiento ?? "";
        }

        if (producto.TipoMovimientoMoraId.HasValue)
        {
            var tmMora = await _context.FI_TipoMovimiento
                .SingleOrDefaultAsync(r => r.Id == producto.TipoMovimientoMoraId.Value, cancellationToken);
            dto.NomTipoMovimientoMora = tmMora?.NomTipoMovimiento ?? "";
        }

        return Result.Success(dto);
    }
}
