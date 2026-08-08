using Yggdrasil.Module.Audit.Features.Audit.Specifications;
using Yggdrasil.Module.Audit.Features.Audit.DTOs;

namespace Yggdrasil.Module.Audit.Features.Audit.Queries;

public class GetAuditsQuery : IQuery<Result<PagedResultDto<AuditListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(AuditListItemDto.Id),
        nameof(AuditListItemDto.RegisteredDate),
        nameof(AuditListItemDto.UserName),
        nameof(AuditListItemDto.AuditEventId),
        nameof(AuditListItemDto.HasError),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(AuditListItemDto.RegisteredDate);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(AuditListItemDto.RegisteredDate);
    }

    public bool SortDescending { get; set; } = true;
    public string? SearchText { get; set; }
    public int? AuditEventId { get; set; }
    public bool? HasError { get; set; }
    public DateTime? RegisteredDateInicial { get; set; }
    public DateTime? RegisteredDateFinal { get; set; }
}

internal class GetAuditsQueryHandler(
    IApplicationDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetAuditsQuery, Result<PagedResultDto<AuditListItemDto>>>
{
    public async Task<Result<PagedResultDto<AuditListItemDto>>> HandleAsync(GetAuditsQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new AuditSpec
            (
                request.SearchText,
                request.AuditEventId,
                request.HasError,
                request.RegisteredDateInicial,
                request.RegisteredDateFinal
            );

            var query = context.SYS_Audit
                .Include(a => a.SYS_AuditEvent)
                .WithSpecification(spec);

            var sortedQuery = sorter.ApplySort(query, request.SortColumn, request.SortDescending);

            var result = await paginator.PaginateAsync<SYS_Audit, AuditListItemDto>(
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
