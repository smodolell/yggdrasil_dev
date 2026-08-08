using Yggdrasil.Module.Catalog.Features.Catalogos.Specifications;
using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Queries;

public class GetTasasIvaQuery : IQuery<Result<PagedResultDto<TasaIvaListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(TasaIvaListItemDto.Id),
        nameof(TasaIvaListItemDto.NomTasaIva),
        nameof(TasaIvaListItemDto.ValorTasa),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(TasaIvaListItemDto.NomTasaIva);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(TasaIvaListItemDto.NomTasaIva);
    }

    public bool SortDescending { get; set; }

    public string? SearchText { get; set; }
    public bool? Activo { get; set; }
}

internal class GetTasasIvaQueryHandler(
    IApplicationDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetTasasIvaQuery, Result<PagedResultDto<TasaIvaListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<TasaIvaListItemDto>>> HandleAsync(GetTasasIvaQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new TasaIvaSpec(request.SearchText, request.Activo);
            var query = _context.CAT_TasaIva.WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);

            var result = await _paginator.PaginateAsync<CAT_TasaIva, TasaIvaListItemDto>(
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
