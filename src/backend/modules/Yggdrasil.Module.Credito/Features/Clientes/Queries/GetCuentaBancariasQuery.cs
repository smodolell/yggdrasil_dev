using Yggdrasil.Module.Credito.Features.Clientes.DTOs;
using Yggdrasil.Module.Credito.Features.Clientes.Specifications;

namespace Yggdrasil.Module.Credito.Features.Clientes.Queries;

public class GetCuentaBancariasQuery : IQuery<Result<PagedResultDto<CuentaBancariaListItemDto>>>
{

    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(CuentaBancariaListItemDto.Id),
        nameof(CuentaBancariaListItemDto.NroCuentaBancaria),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(CuentaBancariaListItemDto.NroCuentaBancaria);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(CuentaBancariaListItemDto.NroCuentaBancaria);
    }

    public bool SortDescending { get; set; }

    public int PersonaId { get; set; }

    public string? SearchText { get; set; }
}


internal class GetCuentaBancariasQueryHandler(IApplicationDbContext context, IDynamicSorter sorter, IPaginator paginator) : IQueryHandler<GetCuentaBancariasQuery, Result<PagedResultDto<CuentaBancariaListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IDynamicSorter _sorter = sorter;
    private readonly IPaginator _paginator = paginator;
    public async Task<Result<PagedResultDto<CuentaBancariaListItemDto>>> HandleAsync(GetCuentaBancariasQuery message, CancellationToken cancellationToken = default)
    {
        var spec = new PersonaCuentaBancariaSpec
  (
            message.SearchText
        );
        var queryable = _context.FI_PersonaCuentaBancaria.WithSpecification(spec);
        var sortedQueryable = _sorter.ApplySort(queryable, message.SortColumn, message.SortDescending);
        var pagedResult = await _paginator.PaginateAsync<FI_PersonaCuentaBancaria, CuentaBancariaListItemDto>
        (
            sortedQueryable,
            message.Page,
            message.PageSize
        );

        return Result.Success(pagedResult);
    }
}