using Yggdrasil.Module.Credito.Features.Configuracion.Perfil.DTOs;
using Yggdrasil.Module.Credito.Features.Configuracion.Perfil.Specifications;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Perfil.Queries;

public class GetSeccionesQuery : IQuery<Result<PagedResultDto<SeccionListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(SeccionListItemDto.Id),
        nameof(SeccionListItemDto.NomSeccion)
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(SeccionListItemDto.Id);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(SeccionListItemDto.Id);
    }

    public bool SortDescending { get; set; } = true;

    public string? SearchText { get; set; }
}

internal class GetSeccionesQueryHandler(
    IApplicationDbContext context,
    IDynamicSorter sorter,
    IPaginator paginator
) : IQueryHandler<GetSeccionesQuery, Result<PagedResultDto<SeccionListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IDynamicSorter _sorter = sorter;
    private readonly IPaginator _paginator = paginator;

    public async Task<Result<PagedResultDto<SeccionListItemDto>>> HandleAsync(
        GetSeccionesQuery message,
        CancellationToken cancellationToken = default)
    {
        var spec = new SeccionSpec(message.SearchText);

        var query = _context.FI_Seccion.WithSpecification(spec);


        var sorterQuery = _sorter.ApplySort(query, message.SortColumn, message.SortDescending);

        var pagedResult = await _paginator.PaginateAsync<FI_Seccion, SeccionListItemDto>
        (
            sorterQuery,
            message.Page,
            message.PageSize
        );

        return Result.Success(pagedResult);
    }
}

