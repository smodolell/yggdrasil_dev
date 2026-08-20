using Yggdrasil.Module.Credito.CS.Features.Creditos.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Creditos.Specifications;

namespace Yggdrasil.Module.Credito.CS.Features.Creditos.Queries;

public class GetCreditosQuery : IQuery<Result<PagedResultDto<CreditoCsListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(CreditoCsListItemDto.Id),
        nameof(CreditoCsListItemDto.ClaveCredito),
        nameof(CreditoCsListItemDto.FechaActivacion),
        nameof(CreditoCsListItemDto.FechaInicio),
        nameof(CreditoCsListItemDto.Capital),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(CreditoCsListItemDto.Id);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(CreditoCsListItemDto.Id);
    }

    public bool SortDescending { get; set; } = true;

    public string? SearchText { get; set; }
    public int? TipoCreditoId { get; set; }
    public int? EstatusCreditoId { get; set; }
    public DateTime? FechaActivacionStart { get; set; }
    public DateTime? FechaActivacionEnd { get; set; }
}

internal class GetCreditosQueryHandler(
    IApplicationDbContext context,
    IDynamicSorter sorter,
    IPaginator paginator
) : IQueryHandler<GetCreditosQuery, Result<PagedResultDto<CreditoCsListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IDynamicSorter _sorter = sorter;
    private readonly IPaginator _paginator = paginator;

    public async Task<Result<PagedResultDto<CreditoCsListItemDto>>> HandleAsync(
        GetCreditosQuery message,
        CancellationToken cancellationToken = default)
    {
        var spec = new CreditoFilterSpecification(
            message.SearchText,
            message.TipoCreditoId,
            message.EstatusCreditoId,
            message.FechaActivacionStart,
            message.FechaActivacionEnd
        );

        var query = _context.CS_Credito.WithSpecification(spec);

        query = _sorter.ApplySort(query, message.SortColumn, message.SortDescending);

        var result = await _paginator.PaginateAsync<CS_Credito, CreditoCsListItemDto>(
            query,
            message.Page,
            message.PageSize,
            cancellationToken
        );

        return Result.Success(result);
    }
}
