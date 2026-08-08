using Yggdrasil.Module.Credito.Features.Clientes.DTOs;
using Yggdrasil.Module.Credito.Features.Clientes.Specifications;

namespace Yggdrasil.Module.Credito.Features.Clientes.Queries;

public class GetTelefonosQuery : IQuery<Result<PagedResultDto<TelefonoListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(TelefonoListItemDto.Id),
        nameof(TelefonoListItemDto.Numero),
        nameof(TelefonoListItemDto.Extension)
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(TelefonoListItemDto.Id);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(TelefonoListItemDto.Id);
    }

    public bool SortDescending { get; set; } = true;

    public int PersonaId { get; set; }

    public string? SearchText { get; set; }

    public int? TelefonoId { get; set; }

    public string? TipoTelefono { get; set; }
}

internal class GetTelefonosQueryHandler(
    IApplicationDbContext context,
    IDynamicSorter sorter,
    IPaginator paginator
) : IQueryHandler<GetTelefonosQuery, Result<PagedResultDto<TelefonoListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IDynamicSorter _sorter = sorter;
    private readonly IPaginator _paginator = paginator;

    public async Task<Result<PagedResultDto<TelefonoListItemDto>>> HandleAsync(
        GetTelefonosQuery message,
        CancellationToken cancellationToken = default)
    {
        var spec = new TelefonoSpec(
            message.PersonaId,
            message.SearchText,
            null
        );

        var queryable = _context.FI_Telefono
            .WithSpecification(spec);


        // Apply sorting
        var sortedQueryable = _sorter.ApplySort(queryable, message.SortColumn, message.SortDescending);

        // Apply pagination
        var pagedResult = await _paginator.PaginateAsync<FI_Telefono, TelefonoListItemDto>
        (
            sortedQueryable,
            message.Page,
            message.PageSize
        );

        return Result.Success(pagedResult);
    }
}