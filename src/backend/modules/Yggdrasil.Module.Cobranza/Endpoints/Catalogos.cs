using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;
using Yggdrasil.Module.Cobranza.Features.Catalogos.DTOs;
using Yggdrasil.Module.Cobranza.Features.Catalogos.Commands;
using Yggdrasil.Module.Cobranza.Features.Catalogos.Queries;

namespace Yggdrasil.Module.Cobranza.Endpoints;

public class Catalogos : EndpointGroupBase
{
    public override string? GroupName => "cob-catalogos";
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Cobranza - Catalogos");

        #region TipoPago
        group.MapGet("tipo-pago/{id}", GetTipoPagoById)
            .WithName("CF_GetTipoPagoById")
            .WithSummary("Obtiene un tipo de pago por ID")
            .Produces<ApiResponseDto<TipoPagoEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tipo-pago/", GetTiposPago)
            .WithSummary("Obtiene tipos de pago filtrados")
            .Produces<ApiResponseDto<List<TipoPagoListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("tipo-pago/", CreateTipoPago)
            .WithName("CF_CreateTipoPago")
            .WithSummary("Crea un nuevo tipo de pago")
            .Accepts<TipoPagoEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("tipo-pago/{id}", UpdateTipoPago)
            .WithName("CF_UpdateTipoPago")
            .WithSummary("Actualiza un tipo de pago")
            .Accepts<TipoPagoEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("tipo-pago/{id}", DeleteTipoPago)
            .WithName("CF_DeleteTipoPago")
            .WithSummary("Elimina un tipo de pago")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion
    }

    #region TipoPago
    public static async Task<IResult> GetTipoPagoById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetTipoPagoByIdQuery { TipoPagoId = id });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetTiposPago(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null)
    {
        var result = await queryMediator.QueryAsync(new GetTipoPagosQuery
        {
            SearchText = q
        });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> CreateTipoPago(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] TipoPagoEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateTipoPagoCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> UpdateTipoPago(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] TipoPagoEditDto model)
    {
        model.TipoPagoId = id;
        var result = await commandMediator.SendAsync(new UpdateTipoPagoCommand(model));
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
