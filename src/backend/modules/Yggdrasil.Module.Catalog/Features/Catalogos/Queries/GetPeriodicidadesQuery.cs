using Yggdrasil.Module.Catalog.Features.Catalogos.Specifications;
using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Queries;

public class GetPeriodicidadesQuery : IQuery<Result<PagedResultDto<PeriodicidadListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(PeriodicidadListItemDto.Id),
        nameof(PeriodicidadListItemDto.ClavePeriodicidad),
        nameof(PeriodicidadListItemDto.NomPeriodicidad),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(PeriodicidadListItemDto.NomPeriodicidad);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(PeriodicidadListItemDto.NomPeriodicidad);
    }

    public bool SortDescending { get; set; }

    public string? SearchText { get; set; }
}

internal class GetPeriodicidadesQueryHandler(
    IApplicationDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetPeriodicidadesQuery, Result<PagedResultDto<PeriodicidadListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<PeriodicidadListItemDto>>> HandleAsync(GetPeriodicidadesQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new PeriodicidadSpec(request.SearchText);
            var query = _context.CAT_Periodicidad.WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);

            var result = await _paginator.PaginateAsync<CAT_Periodicidad, PeriodicidadListItemDto>(
                sortedQuery,
                request.Page,
                request.PageSize,
                cancellationToken
            );
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
