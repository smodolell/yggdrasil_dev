using Yggdrasil.Module.Credito.CS.Features.Catalogos.Specifications;
using Yggdrasil.Module.Credito.CS.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Catalogos.Queries;

public class GetTipoPagosQuery : IQuery<Result<PagedResultDto<TipoPagoCsListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(TipoPagoCsListItemDto.Id),
        nameof(TipoPagoCsListItemDto.NomTipoPago),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(TipoPagoCsListItemDto.NomTipoPago);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(TipoPagoCsListItemDto.NomTipoPago);
    }

    public bool SortDescending { get; set; }

    public string? SearchText { get; set; }
}

internal class GetTipoPagosQueryHandler(
    IApplicationDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetTipoPagosQuery, Result<PagedResultDto<TipoPagoCsListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<TipoPagoCsListItemDto>>> HandleAsync(GetTipoPagosQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new TipoPagoSpec(request.SearchText);
            var query = _context.CS_TipoPago.AsNoTracking().WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);

            var result = await _paginator.PaginateAsync<CS_TipoPago, TipoPagoCsListItemDto>(
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
