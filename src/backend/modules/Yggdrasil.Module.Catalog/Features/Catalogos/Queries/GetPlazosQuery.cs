using Yggdrasil.Module.Catalog.Features.Catalogos.Specifications;
using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Queries;

public class GetPlazosQuery : IQuery<Result<PagedResultDto<PlazoListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(PlazoListItemDto.Id),
        nameof(PlazoListItemDto.ValorPlazo),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(PlazoListItemDto.ValorPlazo);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(PlazoListItemDto.ValorPlazo);
    }

    public bool SortDescending { get; set; }

    public int? ValorPlazo { get; set; }
    public bool? Activo { get; set; }
}

internal class GetPlazosQueryHandler(
    IApplicationDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetPlazosQuery, Result<PagedResultDto<PlazoListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<PlazoListItemDto>>> HandleAsync(GetPlazosQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new PlazoSpec(request.ValorPlazo, request.Activo);
            var query = _context.CAT_Plazo.WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);

            var result = await _paginator.PaginateAsync<CAT_Plazo, PlazoListItemDto>(
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
