using Yggdrasil.Module.Credito.Features.Configuracion.Perfil.DTOs;
using Yggdrasil.Module.Credito.Features.Configuracion.Perfil.Specifications;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Perfil.Queries;

public class GetPerfilesQuery : IQuery<Result<PagedResultDto<PerfilListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(PerfilListItemDto.Id),
        nameof(PerfilListItemDto.NomPerfil),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(PerfilListItemDto.Id);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(PerfilListItemDto.Id);
    }

    public bool SortDescending { get; set; } = true;

    public string? SearchText { get; set; }
    public bool? Activo { get; set; }
}

internal class GetPerfilesQueryHandler(
    IApplicationDbContext context,
    IDynamicSorter sorter,
    IPaginator paginator
) : IQueryHandler<GetPerfilesQuery, Result<PagedResultDto<PerfilListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IDynamicSorter _sorter = sorter;
    private readonly IPaginator _paginator = paginator;

    public async Task<Result<PagedResultDto<PerfilListItemDto>>> HandleAsync(
        GetPerfilesQuery message,
        CancellationToken cancellationToken = default)
    {
        var spec = new PerfilSpec(
            message.SearchText,
            message.Activo
        );
         
        var query = _context.FI_Perfil.WithSpecification(spec);

        var sorterQuery = _sorter.ApplySort(query, message.SortColumn, message.SortDescending);

        var pagedResult = await _paginator.PaginateAsync<FI_Perfil, PerfilListItemDto>
        (
            sorterQuery,
            message.Page,
            message.PageSize
        );

        return Result.Success(pagedResult);
    }
}