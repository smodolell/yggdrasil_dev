using Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;
using Yggdrasil.Module.Credito.Features.Configuracion.Producto.Specifications;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Queries;

public class GetProductosQuery : IQuery<Result<PagedResultDto<ProductoListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(ProductoListItemDto.Id),
        nameof(ProductoListItemDto.NomProducto),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(ProductoListItemDto.NomProducto);

    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value switch
        {
            < 1 => 10,
            > 100 => 100,
            _ => value
        };
    }

    public string SortColumn
    {
        get => _sortColumn;
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(ProductoListItemDto.NomProducto);
    }

    public bool SortDescending { get; set; }

    public string? SearchText { get; set; }
}

internal class ListProductoQueryHandler(
    IApplicationDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetProductosQuery, Result<PagedResultDto<ProductoListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<ProductoListItemDto>>> HandleAsync(GetProductosQuery message, CancellationToken cancellationToken = default)
    {
        var spec = new ProductoSpec(message.SearchText);
        var query = _context.FI_Producto.WithSpecification(spec);
        var sortedQuery = _sorter.ApplySort(query, message.SortColumn, message.SortDescending);
        var result = await _paginator.PaginateAsync<FI_Producto, ProductoListItemDto>(
              sortedQuery,
              message.Page,
              message.PageSize,
              cancellationToken
          );
        return Result.Success(result);
    }
}
