using Yggdrasil.Module.Credito.Features.Configuracion.CalendarioLaboral.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.CalendarioLaboral.Queries;

public class GetCalendarioLaboralQuery : IQuery<Result<PagedResultDto<CalendarioLaboralListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns = new()
    {
        nameof(CalendarioLaboralListItemDto.Id),
        nameof(CalendarioLaboralListItemDto.Fecha),
        nameof(CalendarioLaboralListItemDto.EsHabil),
    };

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(CalendarioLaboralListItemDto.Fecha);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(CalendarioLaboralListItemDto.Fecha);
    }

    public bool SortDescending { get; set; }
    public int? Anio { get; set; }
    public int? Mes { get; set; }
}

internal class GetCalendarioLaboralQueryHandler(
    IApplicationDbContext context,
    IPaginator paginator,
    IDynamicSorter sorter
) : IQueryHandler<GetCalendarioLaboralQuery, Result<PagedResultDto<CalendarioLaboralListItemDto>>>
{
    public async Task<Result<PagedResultDto<CalendarioLaboralListItemDto>>> HandleAsync(
        GetCalendarioLaboralQuery request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = context.CAT_CalendarioLaboral.AsQueryable();

            if (request.Anio.HasValue)
            {
                query = query.Where(x => x.Fecha.Year == request.Anio.Value);
            }

            if (request.Mes.HasValue)
            {
                query = query.Where(x => x.Fecha.Month == request.Mes.Value);
            }

            var sorted = sorter.ApplySort(query, request.SortColumn, request.SortDescending);

            var result = await paginator.PaginateAsync<CAT_CalendarioLaboral, CalendarioLaboralListItemDto>(
                sorted, request.Page, request.PageSize, cancellationToken);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error al obtener el calendario laboral: {ex.Message}");
        }
    }
}
