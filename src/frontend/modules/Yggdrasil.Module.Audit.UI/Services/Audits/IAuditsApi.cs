
using Refit;
using Yggdrasil.Blazor.DTOs;
using Yggdrasil.Module.Audit.UI.Services.Audits.DTOs;

namespace Yggdrasil.Module.Audit.UI.Services.Audits;


public interface IAuditsApi
{
    [Get("/api/audits/audit/")]
    Task<ApiResponseDto<PagedResultDto<AuditListItemDto>>> GetAudits(
        [Query] string? q = null,
        [Query] int? auditEventId = null,
        [Query] bool? hasError = null,
        [Query] DateTime? fechaInicio = null,
        [Query] DateTime? fechaFin = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "RegisteredDate",
        [Query] bool sortDescending = true,
        CancellationToken cancellationToken = default);

    [Get("/api/audits/audit/report")]
    Task<ApiResponseDto<AuditReportDto>> GetAuditReport(
        [Query] int? anio = null,
        [Query] int? mes = null,
        [Query] string? userName = null,
        CancellationToken cancellationToken = default);
}