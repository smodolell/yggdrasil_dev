using Yggdrasil.Module.Audit.Features.LoginLog.Specifications;
using Yggdrasil.Module.Audit.Features.LoginLog.DTOs;

namespace Yggdrasil.Module.Audit.Features.LoginLog.Queries;

// PENDIENTE: Requiere agregar SYS_LoginLog al dominio y DbSet<SYS_LoginLog> en IApplicationDbContext
public class GetLoginLogsQuery : IQuery<Result<PagedResultDto<LoginLogDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(LoginLogDto.Id),
        nameof(LoginLogDto.UserName),
        nameof(LoginLogDto.Time),
        nameof(LoginLogDto.IsSuccessd),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(LoginLogDto.Time);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(LoginLogDto.Time);
    }

    public bool SortDescending { get; set; } = true;
    public string? SearchText { get; set; }
    public bool? IsSuccessd { get; set; }
    public DateTime? TimeStart { get; set; }
    public DateTime? TimeEnd { get; set; }
}

internal class GetLoginLogsQueryHandler(
    IApplicationDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetLoginLogsQuery, Result<PagedResultDto<LoginLogDto>>>
{
    public async Task<Result<PagedResultDto<LoginLogDto>>> HandleAsync(GetLoginLogsQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new LoginLogSpec
            (
                request.SearchText,
                request.IsSuccessd,
                request.TimeStart,
                request.TimeEnd
            );

            var query = context.SYS_LoginLog.WithSpecification(spec);
            var sortedQuery = sorter.ApplySort(query, request.SortColumn, request.SortDescending);

            var result = await paginator.PaginateAsync<SYS_LoginLog, LoginLogDto>(
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
