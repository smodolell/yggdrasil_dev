using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Credito.CS.Features.Creditos.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Creditos.Queries;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.Credito.CS.Endpoints;

public class Creditos : EndpointGroupBase
{
    public override string? GroupName => "cs-creditos";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("CreditoCS - Creditos");

        group.MapGet("credito/", GetCreditos)
         .WithName("GetCSCreditos")
         .WithSummary("Obtiene créditos simples filtrados y paginados")
         .Produces<ApiResponseDto<PagedResultDto<CreditoCsListItemDto>>>(StatusCodes.Status200OK)
         .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
         .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("credito/{id}/detail", GetCreditoDetail)
            .WithName("GetCSCreditoDetail")
            .WithSummary("Obtiene el detalle completo de un crédito simple")
            .Produces<ApiResponseDto<CreditoCsDetailDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("credito/{id}/tabla-amortiza", GetTablaAmortiza)
           .WithName("GetCSTablaAmortiza")
           .WithSummary("Obtiene la tabla de amortización de un crédito simple")
           .Produces<ApiResponseDto<List<TablaAmortizaCsItemDto>>>(StatusCodes.Status200OK)
           .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
           .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
           .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    public static async Task<IResult> GetCreditos(
      [FromServices] IQueryMediator queryMediator,
      [FromQuery] string? q = null,
      [FromQuery] int page = 1,
      [FromQuery] int pageSize = 10,
      [FromQuery] string? sortColumn = null,
      [FromQuery] bool sortDesc = true,
      [FromQuery] int? tipoCreditoId = null,
      [FromQuery] int? estatusCreditoId = null,
      [FromQuery] DateTime? fechaActivacionStart = null,
      [FromQuery] DateTime? fechaActivacionEnd = null)
    {
        var query = new GetCreditosQuery
        {
            SearchText = q,
            Page = page,
            PageSize = pageSize,
            SortDescending = sortDesc,
            TipoCreditoId = tipoCreditoId,
            EstatusCreditoId = estatusCreditoId,
            FechaActivacionStart = fechaActivacionStart,
            FechaActivacionEnd = fechaActivacionEnd
        };
        if (sortColumn != null) query.SortColumn = sortColumn;
        var result = await queryMediator.QueryAsync(query);
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetCreditoDetail(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetCreditoDetailQuery { Id = id });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetTablaAmortiza(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id,
        [FromQuery] int? version = null
    )
    {
        var result = await queryMediator.QueryAsync(new GetTablaAmortizaQuery(id, version));
        return result.ToCustomMinimalApiResult();
    }
}
