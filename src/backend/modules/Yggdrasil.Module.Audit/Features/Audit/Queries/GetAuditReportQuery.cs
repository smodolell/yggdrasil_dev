using Yggdrasil.Module.Audit.Features.Audit.Specifications;
using Yggdrasil.Module.Audit.Features.Audit.DTOs;

namespace Yggdrasil.Module.Audit.Features.Audit.Queries;

public class GetAuditReportQuery : IQuery<Result<AuditReportDto>>
{
    public int? Anio { get; set; }
    public int? Mes { get; set; }
    public string? UserName { get; set; }
}

internal class GetAuditReportQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetAuditReportQuery, Result<AuditReportDto>>
{
    public async Task<Result<AuditReportDto>> HandleAsync(GetAuditReportQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var spec = new AuditReportSpec
            (
                request.Anio,
                request.Mes,
                request.UserName
            );

            var query = context.SYS_Audit
                .Include(a => a.SYS_AuditEvent)
                .WithSpecification(spec);

            var items = await query
                .GroupBy(g => new { g.UserName, g.AuditEventId })
                .Select(s => new AuditReportItemDto
                {
                    UserName = s.Key.UserName,
                    AuditEventId = s.Key.AuditEventId,
                    Cantidad = s.Count()
                })
                .ToListAsync(cancellationToken);

            var columnas = await query
                .GroupBy(g => new { g.AuditEventId, g.SYS_AuditEvent.Description })
                .Select(s => new AuditReportColumnDto
                {
                    AuditEventId = s.Key.AuditEventId,
                    AuditEvent = s.Key.Description
                })
                .ToListAsync(cancellationToken);

            var result = new AuditReportDto
            {
                Anio = request.Anio ?? DateTime.Now.Year,
                Mes = request.Mes ?? DateTime.Now.Month,
                UserName = request.UserName,
                Items = items,
                Usuarios = items.Select(i => i.UserName).Distinct().ToList(),
                Columnas = columnas
            };

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
