using Yggdrasil.Module.Credito.Features.Clientes.DTOs;
using Yggdrasil.Module.Credito.Features.Clientes.Specifications;

namespace Yggdrasil.Module.Credito.Features.Clientes.Queries;

public class GetCreditosQuery : IQuery<Result<PagedResultDto<CreditoListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(CreditoListItemDto.Id),
        nameof(CreditoListItemDto.ClaveCredito),

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

    public int PersonaId { get; set; }
    public int? ProductoId { get; set; }
    public int? EstatusCreditoId { get; set; }

    public string? SearchText { get; set; }
}

internal class GetCreditosQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IDynamicSorter sorter,
    IPaginator paginator
) : IQueryHandler<GetCreditosQuery, Result<PagedResultDto<CreditoListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IDynamicSorter _sorter = sorter;
    private readonly IPaginator _paginator = paginator;

    public async Task<Result<PagedResultDto<CreditoListItemDto>>> HandleAsync(
        GetCreditosQuery message,
        CancellationToken cancellationToken = default)
    {
        var spec = new CreditoByPersonaIdSpec
        (
            message.PersonaId,
            message.ProductoId,
            message.SearchText,
            message.EstatusCreditoId
        );

        var queryable = _context.FI_Credito.WithSpecification(spec);

        var sortedQueryable = _sorter.ApplySort(queryable, message.SortColumn, message.SortDescending);

        var pagedResult = await _paginator.PaginateAsync<FI_Credito, CreditoListItemDto>
        (
            sortedQueryable,
            message.Page,
            message.PageSize
        );

        return Result.Success(pagedResult);
    }
}