using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Credito.CS.Features.Catalogos.Commands;
using Yggdrasil.Module.Credito.CS.Features.Catalogos.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Catalogos.Queries;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.Credito.CS.Endpoints;

public class Catalogos : EndpointGroupBase
{
    public override string? GroupName => "cs-catalogos";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("CreditoCS - Catalogos");

        #region TipoPago
        group.MapGet("tipo-pago/{id}", GetTipoPagoById)
            .WithName("GetCSTipoPagoById")
            .WithSummary("Obtiene un tipo de pago de crédito simple por ID")
            .Produces<ApiResponseDto<TipoPagoCsEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tipo-pago/", GetTipoPagos)
            .WithName("GetCSTipoPagos")
            .WithSummary("Obtiene tipos de pago de crédito simple paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<TipoPagoCsListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("tipo-pago/", CreateTipoPago)
            .WithName("CreateCSTipoPago")
            .WithSummary("Crea un nuevo tipo de pago de crédito simple")
            .Accepts<TipoPagoCsEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("tipo-pago/{id}", UpdateTipoPago)
            .WithName("UpdateCSTipoPago")
            .WithSummary("Actualiza un tipo de pago de crédito simple")
            .Accepts<TipoPagoCsEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("tipo-pago/{id}", DeleteTipoPago)
            .WithName("DeleteCSTipoPago")
            .WithSummary("Elimina un tipo de pago de crédito simple")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion
    }

    #region TipoPago
    public static async Task<IResult> GetTipoPagoById(
        [FromServices] IQueryMediator queryMediator,
        int id)
    {
        var result = await queryMediator.QueryAsync(new GetTipoPagoByIdQuery { TipoPagoId = id });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetTipoPagos(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(TipoPagoCsListItemDto.Id),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetTipoPagosQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> CreateTipoPago(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] TipoPagoCsEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateTipoPagoCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> UpdateTipoPago(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] TipoPagoCsEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateTipoPagoCommand { TipoPagoId = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> DeleteTipoPago(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteTipoPagoCommand { TipoPagoId = id });
        return result.ToCustomMinimalApiResult();
    }
    #endregion
}
