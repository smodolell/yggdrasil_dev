using Yggdrasil.Module.Credito.CS.Features.Configuracion.Specifications;
using Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.Queries;

public class GetTipoCreditosQuery : IQuery<Result<PagedResultDto<TipoCreditoListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(TipoCreditoListItemDto.Id),
        nameof(TipoCreditoListItemDto.ClaveTipoCredito),
        nameof(TipoCreditoListItemDto.NomTipoCredito),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(TipoCreditoListItemDto.NomTipoCredito);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(TipoCreditoListItemDto.NomTipoCredito);
    }

    public bool SortDescending { get; set; }

    public string? SearchText { get; set; }
}

internal class GetTipoCreditosQueryHandler(
    IApplicationDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetTipoCreditosQuery, Result<PagedResultDto<TipoCreditoListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<TipoCreditoListItemDto>>> HandleAsync(GetTipoCreditosQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new TipoCreditoSpec(request.SearchText);
            var query = _context.CS_TipoCredito.AsNoTracking().WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);

            var result = await _paginator.PaginateAsync<CS_TipoCredito, TipoCreditoListItemDto>(
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
