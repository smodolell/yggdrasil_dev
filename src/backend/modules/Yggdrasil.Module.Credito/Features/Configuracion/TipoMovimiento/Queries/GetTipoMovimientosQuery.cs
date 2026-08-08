using Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.DTOs;
using Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.Specifications;

namespace Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.Queries;

public class GetTipoMovimientosQuery : IQuery<Result<PagedResultDto<TipoMovimientoListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(TipoMovimientoListItemDto.Id),
        nameof(TipoMovimientoListItemDto.Clave),
        nameof(TipoMovimientoListItemDto.NomTipoMovimiento),
        nameof(TipoMovimientoListItemDto.Activo),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(TipoMovimientoListItemDto.Id);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(TipoMovimientoListItemDto.Id);
    }

    public bool SortDescending { get; set; } = true;

    public bool? Activo { get; set; }
    public string? SearchText { get; set; }
}

internal class GetTipoMovimientosHandler(
    IApplicationDbContext context,
    IDynamicSorter sorter,
    IPaginator paginator
) : IQueryHandler<GetTipoMovimientosQuery, Result<PagedResultDto<TipoMovimientoListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IDynamicSorter _sorter = sorter;
    private readonly IPaginator _paginator = paginator;

    public async Task<Result<PagedResultDto<TipoMovimientoListItemDto>>> HandleAsync(
        GetTipoMovimientosQuery message,
        CancellationToken cancellationToken = default)
    {
        var spec = new TipoMovimientoSpec(
            message.SearchText,
            message.Activo
        );

        var query = _context.FI_TipoMovimiento
            .WithSpecification(spec);


        var sorterQuery = _sorter.ApplySort(query, message.SortColumn, message.SortDescending);

        var pagedResult = await _paginator.PaginateAsync<FI_TipoMovimiento, TipoMovimientoListItemDto>
        (
            sorterQuery,
            message.Page,
            message.PageSize
        );

        return Result.Success(pagedResult);
    }
}