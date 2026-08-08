using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Audit.Features.Audit.Queries;
using Yggdrasil.Module.Audit.Features.Audit.DTOs;
using IResult = Microsoft.AspNetCore.Http.IResult;
using Yggdrasil.Common.DTOs;

namespace Yggdrasil.Module.Audit.Endpoints;

public class Audits : EndpointGroupBase
{
    public override string? GroupName => "audits";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Auditoria");

        #region Audit
        group.MapGet("audit/", GetAudits)
            .WithSummary("Obtiene registros de auditoría paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<AuditListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("audit/report", GetAuditReport)
            .WithName("GetAuditReport")
            .WithSummary("Obtiene el reporte de auditoría agrupado por usuario y evento")
            .Produces<ApiResponseDto<AuditReportDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion
    }

    #region Audit
    public async Task<IResult> GetAudits(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int? auditEventId = null,
        [FromQuery] bool? hasError = null,
        [FromQuery] DateTime? fechaInicio = null,
        [FromQuery] DateTime? fechaFin = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(AuditListItemDto.RegisteredDate),
        [FromQuery] bool sortDescending = true)
    {
        var result = await queryMediator.QueryAsync(new GetAuditsQuery
        {
            SearchText = q,
            AuditEventId = auditEventId,
            HasError = hasError,
            RegisteredDateInicial = fechaInicio,
            RegisteredDateFinal = fechaFin,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetAuditReport(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] int? anio = null,
        [FromQuery] int? mes = null,
        [FromQuery] string? userName = null)
    {
        var result = await queryMediator.QueryAsync(new GetAuditReportQuery
        {
            Anio = anio,
            Mes = mes,
            UserName = userName
        });
        return result.ToCustomMinimalApiResult();
    }
    #endregion
}
