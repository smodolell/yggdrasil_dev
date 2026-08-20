using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Credito.CS.Features.SelectLists;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.Credito.CS.Endpoints;

public class SelectLists : EndpointGroupBase
{
    public override string? GroupName => "cs-select-lists";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("CreditoCS - SelectLists");

        group.MapGet("tipos-credito", GetTipoCreditoSelectList)
            .WithName("GetCSTipoCreditoSelectList")
            .WithSummary("Obtiene Tipos de Crédito")
            .WithDescription("Retorna una lista de Tipos de Crédito de crédito simple")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("metodos-armotizacion", GetMetodoArmotizacionSelectList)
            .WithName("GetCSMetodoArmotizacionSelectList")
            .WithSummary("Obtiene Métodos de Amortización")
            .WithDescription("Retorna una lista de Métodos de Amortización activos")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tipos-movimiento", GetTipoMovimientoSelectList)
            .WithName("GetCSTipoMovimientoSelectList")
            .WithSummary("Obtiene Tipos de Movimiento")
            .WithDescription("Retorna una lista de Tipos de Movimiento de crédito simple activos")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    public static async Task<IResult> GetTipoCreditoSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetTipoCreditoSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetMetodoArmotizacionSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetMetodoArmotizacionSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetTipoMovimientoSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetTipoMovimientoSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }
}
