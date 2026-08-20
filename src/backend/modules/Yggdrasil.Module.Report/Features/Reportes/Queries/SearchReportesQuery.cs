using Yggdrasil.Module.Report.Features.Reportes.Specifications;
using Yggdrasil.Module.Report.Features.Reportes.DTOs;

namespace Yggdrasil.Module.Report.Features.Reportes.Queries;

public class SearchReportesQuery : IQuery<Result<List<SelectReporteDto>>>
{
    public string? SearchText { get; set; }
}

internal class SearchReportesQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<SearchReportesQuery, Result<List<SelectReporteDto>>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<List<SelectReporteDto>>> HandleAsync(SearchReportesQuery message, CancellationToken cancellationToken = default)
    {
        var spec = new ReporteSpec (message.SearchText);

        var data = await _context.RSP_Reporte
            .WithSpecification(spec)
            .Select(r => new SelectReporteDto
            {
                ReporteId = r.Id,
                NomReporte = r.NomReporte
            })
            .ToListAsync(cancellationToken);

        return Result.Success(data);
    }
}
