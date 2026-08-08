using Yggdrasil.Module.Catalog.Features.Catalogos.Specifications;
using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Queries;

public class GetTasasFijasQuery : IQuery<Result<PagedResultDto<TasaListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(TasaListItemDto.Id),
        nameof(TasaListItemDto.NomTasa),
        nameof(TasaListItemDto.ValorTasa),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(TasaListItemDto.NomTasa);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(TasaListItemDto.NomTasa);
    }

    public bool SortDescending { get; set; }

    public string? SearchText { get; set; }
    public decimal? ValueMin { get; set; }
    public decimal? ValueMax { get; set; }
    public bool? Activo { get; set; }
}

internal class GetTasasFijasQueryHandler(
    IApplicationDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetTasasFijasQuery, Result<PagedResultDto<TasaListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<TasaListItemDto>>> HandleAsync(GetTasasFijasQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new TasaFijaSpec(
                request.ValueMin,
                request.ValueMax,
                request.SearchText,
                request.Activo
            );
            var query = _context.CAT_Tasa.WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);

            var result = await _paginator.PaginateAsync<CAT_Tasa, TasaListItemDto>(
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
