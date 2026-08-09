using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Credito.Features.Creditos.DTOs;
using Yggdrasil.Module.Credito.Features.Creditos.Queries;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.Credito.Endpoints;

public class Creditos : EndpointGroupBase
{
    public override string? GroupName => "fi-creditos";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Crédito - Créditos");

        group.MapGet("credito/", GetCreditos)
         .WithName("GetCreditos")
         .WithSummary("Obtiene créditos filtrados y paginados")
         .Produces<ApiResponseDto<PagedResultDto<CreditoListItemDto>>>(StatusCodes.Status200OK)
         .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
         .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("credito/{id}/detail", GetCreditoDetail)
            .WithName("GetCreditoDetail")
            .WithSummary("Obtiene el detalle completo de un crédito (crédito, cliente y producto)")
            .Produces<ApiResponseDto<CreditoDetailDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("credito/{id}/tabla-amortiza", GetTablaAmortiza)
           .WithName("CF_GetTablaAmortiza")
           .WithSummary("Obtiene la tabla de amortización de un crédito")
           .Produces<ApiResponseDto<List<TablaAmortizaItemDto>>>(StatusCodes.Status200OK)
           .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
           .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
           .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("credito/{id}/movimientos", GetMovimientos)
           .WithName("CF_GetMovimientos")
           .WithSummary("Obtiene los movimientos de un crédito")
           .Produces<ApiResponseDto<List<MovimientoItemDto>>>(StatusCodes.Status200OK)
           .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
           .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
           .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("credito/pagos", GetPagos)
            .WithName("GetPagos")
            .WithSummary("Obtiene los pagos de un crédito (creditoId) o de todos los créditos de una persona (personaId)")
            .Produces<ApiResponseDto<List<PagoItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("credito/pagos/{pagoId}/detail", GetPagoDetailById)
            .WithName("GetPagoDetailById")
            .WithSummary("Obtiene el detalle de un pago")
            .Produces<ApiResponseDto<PagoDetailDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    public async Task<IResult> GetCreditos(
      [FromServices] IQueryMediator queryMediator,
      [FromQuery] string? q = null,
      [FromQuery] int page = 1,
      [FromQuery] int pageSize = 10,
      [FromQuery] string? sortColumn = null,
      [FromQuery] bool sortDesc = true,
      [FromQuery] int? productoId = null,
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
            ProductoId = productoId,
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

    public static async Task<IResult> GetMovimientos(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id
    )
    {
        var result = await queryMediator.QueryAsync(new GetMovimientosQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetPagos(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] int? personaId = null,
        [FromQuery] int? creditoId = null
    )
    {
        var result = await queryMediator.QueryAsync(new GetPagosQuery(personaId, creditoId));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetPagoDetailById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int pagoId
    )
    {
        var result = await queryMediator.QueryAsync(new GetPagoDetailByIdQuery(pagoId));
        return result.ToCustomMinimalApiResult();
    }
}
