using Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;
using Yggdrasil.Module.Cobranza.Features.Intradias.Specifications;

namespace Yggdrasil.Module.Cobranza.Features.Intradias.Queries;

public class GetCreditosIntraDiaQuery : IQuery<Result<PagedResultDto<CreditoIntraDiaListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(CreditoIntraDiaListItemDto.Id),
        nameof(CreditoIntraDiaListItemDto.MontoOtorgado),
        nameof(CreditoIntraDiaListItemDto.MontoOtorgado),
        nameof(CreditoIntraDiaListItemDto.Capital),
        nameof(CreditoIntraDiaListItemDto.Tasa),
        nameof(CreditoIntraDiaListItemDto.TasaIva),
        nameof(CreditoIntraDiaListItemDto.FechaPrimeraRenta),
        nameof(CreditoIntraDiaListItemDto.Estado),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(CreditoIntraDiaListItemDto.Id);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(CreditoIntraDiaListItemDto.Id);
    }

    public bool SortDescending { get; set; } = true;

    public DateTime? FechaPrimeraRentaStart { get; set; }
    public DateTime? FechaPrimeraRentaEnd { get; set; }
}

internal class GetCreditosIntraDiaQueryHandler(
    IApplicationDbContext context,
    IDynamicSorter sorter,
    IPaginator paginator
) : IQueryHandler<GetCreditosIntraDiaQuery, Result<PagedResultDto<CreditoIntraDiaListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IDynamicSorter _sorter = sorter;
    private readonly IPaginator _paginator = paginator;

    public async Task<Result<PagedResultDto<CreditoIntraDiaListItemDto>>> HandleAsync(
        GetCreditosIntraDiaQuery message,
        CancellationToken cancellationToken = default)
    {
        var spec = new CreditoIntraDiaFilterSpecification(
            message.FechaPrimeraRentaStart,
            message.FechaPrimeraRentaEnd
        );

        var query = _context.DEV_CreditoIntraDia.WithSpecification(spec);

        query = _sorter.ApplySort(query, message.SortColumn, message.SortDescending);

        var result = await _paginator.PaginateAsync<DEV_CreditoIntraDia, CreditoIntraDiaListItemDto>(
            query,
            message.Page,
            message.PageSize,
            cancellationToken
        );

        return Result.Success(result);
    }
}
