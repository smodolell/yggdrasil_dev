using Yggdrasil.Module.Report.Features.Reportes.Specifications;
using Yggdrasil.Module.Report.Features.Reportes.DTOs;

namespace Yggdrasil.Module.Report.Features.Reportes.Queries;

public class GetParametrosQuery : IQuery<Result<List<ParametroListItemDto>>>
{
    public int? ReporteId { get; set; }
    public string? SearchText { get; set; }
}

internal class GetParametrosQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetParametrosQuery, Result<List<ParametroListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<List<ParametroListItemDto>>> HandleAsync(GetParametrosQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new ParametroSpec(request.ReporteId, request.SearchText);
            

            var data = await _context.RSP_Parametro
                .Include(p => p.RSP_Input)
                .WithSpecification(spec)
                .OrderBy(o => o.Order)
                .Select(r => new ParametroListItemDto
                {
                    Id = r.Id,
                    NomParametro = r.NomParametro,
                    TipoDato = r.TipoDato,
                    NomInput = r.RSP_Input.NomInput,
                    TablaRef = r.TablaRef,
                    ColumnaValor = r.ColumnaValor,
                    ColumnaTexto = r.ColumnaTexto,
                    Display = r.Display,
                    Order = r.Order
                })
                .ToListAsync(cancellationToken);

            return Result.Success(data);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
