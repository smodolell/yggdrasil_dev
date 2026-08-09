using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;
using Yggdrasil.Module.Catalog.Features.Catalogos.Specifications;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Queries;

public class GetBancosQuery : IQuery<Result<PagedResultDto<BancoListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(BancoListItemDto.Id),
        nameof(BancoListItemDto.NomBanco),
        nameof(BancoListItemDto.CodigoBCRA),
        nameof(BancoListItemDto.CBUPrefix),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(BancoListItemDto.NomBanco);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(BancoListItemDto.NomBanco);
    }

    public bool SortDescending { get; set; }

    public string? SearchText { get; set; }
}

internal class GetBancosQueryHandler(
    IApplicationDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetBancosQuery, Result<PagedResultDto<BancoListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<BancoListItemDto>>> HandleAsync(GetBancosQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new BancoSpec(request.SearchText);

            var query = _context.CAT_Banco.WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);

            var result = await _paginator.PaginateAsync<CAT_Banco, BancoListItemDto>(
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
