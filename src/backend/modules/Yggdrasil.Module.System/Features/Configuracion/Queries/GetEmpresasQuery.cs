using Yggdrasil.Module.System.Features.Configuracion.Specifications;
using Yggdrasil.Module.System.Features.Configuracion.DTOs;

namespace Yggdrasil.Module.System.Features.Configuracion.Queries;

public class GetEmpresasQuery : IQuery<Result<PagedResultDto<EmpresaListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns = new()
    {
        nameof(EmpresaListItemDto.Id),
        nameof(EmpresaListItemDto.NomEmpresa),
    };

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(EmpresaListItemDto.NomEmpresa);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(EmpresaListItemDto.NomEmpresa);
    }

    public bool SortDescending { get; set; }

    public string? SearchText { get; set; }
}


internal class GetEmpresasQueryHandler(
    IApplicationDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter


) : IQueryHandler<GetEmpresasQuery, Result<PagedResultDto<EmpresaListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IPaginator _paginator = paginator;
    private readonly IDynamicSorter _sorter = sorter;

    public async Task<Result<PagedResultDto<EmpresaListItemDto>>> HandleAsync(GetEmpresasQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new EmpresaSpec(request.SearchText);

            var query = _context.CAT_Empresa.WithSpecification(spec);
            var sortedQuery = _sorter.ApplySort(query, request.SortColumn, request.SortDescending);

            var result = await _paginator.PaginateAsync<CAT_Empresa, EmpresaListItemDto>(
                sortedQuery,
                request.Page,
                request.PageSize
            );
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}