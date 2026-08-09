using Yggdrasil.Module.Credito.Features.Creditos.DTOs;
using Yggdrasil.Module.Credito.Features.Creditos.Specifications;

namespace Yggdrasil.Module.Credito.Features.Creditos.Queries;

public class GetCreditosQuery : IQuery<Result<PagedResultDto<CreditoListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(CreditoListItemDto.Id),
        nameof(CreditoListItemDto.ClaveCredito),
        nameof(CreditoListItemDto.FechaActivacion),
        nameof(CreditoListItemDto.FechaInicio),
        nameof(CreditoListItemDto.Capital),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(CreditoListItemDto.Id);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(CreditoListItemDto.Id);
    }

    public bool SortDescending { get; set; } = true;

    public string? SearchText { get; set; }
    public int? ProductoId { get; set; }
    public int? EstatusCreditoId { get; set; }
    public DateTime? FechaActivacionStart { get; set; }
    public DateTime? FechaActivacionEnd { get; set; }
}

internal class GetCreditosQueryHandler(
    IApplicationDbContext context,
    IDynamicSorter sorter,
    IPaginator paginator
) : IQueryHandler<GetCreditosQuery, Result<PagedResultDto<CreditoListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IDynamicSorter _sorter = sorter;
    private readonly IPaginator _paginator = paginator;

    public async Task<Result<PagedResultDto<CreditoListItemDto>>> HandleAsync(
        GetCreditosQuery message,
        CancellationToken cancellationToken = default)
    {
        var spec = new CreditoFilterSpecification(
            message.SearchText,
            message.ProductoId,
            message.EstatusCreditoId,
            message.FechaActivacionStart,
            message.FechaActivacionEnd
        );

        var query = _context.FI_Credito.WithSpecification(spec);

        query = _sorter.ApplySort(query, message.SortColumn, message.SortDescending);

        var result = await _paginator.PaginateAsync<FI_Credito, CreditoListItemDto>(
            query,
            message.Page,
            message.PageSize
        );

        return Result.Success(result);
    }
}
