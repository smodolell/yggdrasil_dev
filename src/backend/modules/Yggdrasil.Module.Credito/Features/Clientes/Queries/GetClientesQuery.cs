using Yggdrasil.Module.Credito.Features.Clientes.DTOs;
using Yggdrasil.Module.Credito.Features.Clientes.Specifications;

namespace Yggdrasil.Module.Credito.Features.Clientes.Queries;

public class GetClientesQuery : IQuery<Result<PagedResultDto<PersonaListItemDto>>>
{
    private static readonly HashSet<string> _validSortColumns =
    [
        nameof(PersonaListItemDto.Id),
        nameof(PersonaListItemDto.NombreCliente),
        nameof(PersonaListItemDto.FechaAltaCliente),
    ];

    private int _page = 1;
    private int _pageSize = 10;
    private string _sortColumn = nameof(PersonaListItemDto.FechaAltaCliente);

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
        set => _sortColumn = _validSortColumns.Contains(value) ? value : nameof(PersonaListItemDto.FechaAltaCliente);
    }

    public bool SortDescending { get; set; }



    public string? SearchText { get; set; }
    public int? PerfilId { get; set; }
    public int? GeneroId { get; set; }
    public int? EdoCivilId { get; set; }
    public string? LugarNacimientoId { get; set; }
    public DateTime? FechaAltaClienteStart { get; set; }
    public DateTime? FechaAltaClienteEnd { get; set; }

}
internal class GetClientesQueryHandler(IApplicationDbContext context, IDynamicSorter sorter, IPaginator paginator) : IQueryHandler<GetClientesQuery, Result<PagedResultDto<PersonaListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IDynamicSorter _sorter = sorter;
    private readonly IPaginator _paginator = paginator;

    public async Task<Result<PagedResultDto<PersonaListItemDto>>> HandleAsync(GetClientesQuery message, CancellationToken cancellationToken = default)
    {
        var spec = new PersonaSpec
        (
            message.SearchText,
            message.PerfilId,
            message.GeneroId,
            message.EdoCivilId,
            //message.LugarNacimientoId,
            message.FechaAltaClienteStart,
            message.FechaAltaClienteEnd
        );

        var query = _context.FI_Persona.WithSpecification(spec);
        if (message.SortColumn == nameof(PersonaListItemDto.NombreCliente))
        {
            query = query.OrderBy(p => p.PrimerNombre)
                         .ThenBy(p => p.SegundoNombre)
                         .ThenBy(p => p.ApellidoPaterno)
                         .ThenBy(p => p.ApellidoMaterno);
        }
        else
        {
            query = _sorter.ApplySort(query, message.SortColumn, message.SortDescending);
        }

        var result = await _paginator.PaginateAsync<FI_Persona, PersonaListItemDto>(query, message.Page, message.PageSize);

        return Result.Success(result);
    }
}