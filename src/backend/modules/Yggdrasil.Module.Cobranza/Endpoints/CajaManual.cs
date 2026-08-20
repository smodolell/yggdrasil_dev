using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;
using Yggdrasil.Module.Cobranza.Features.CajaManual.DTOs;
using Yggdrasil.Module.Cobranza.Features.CajaManual.Commands;
using Yggdrasil.Module.Cobranza.Features.CajaManual.Queries;

namespace Yggdrasil.Module.Cobranza.Endpoints;

public class CajaManual : EndpointGroupBase
{
    public override string? GroupName => "cob-caja-manual";
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Cobranza");

        group.MapGet("caja-manual/", GetCajaManual)
            .WithName("Cob_GetCajaManual")
            .WithSummary("Obtiene la plantilla de caja manual para una persona o un crédito")
            .Produces<ApiResponseDto<CajaManualDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("caja-manual/movimientos-pendientes/{creditoId}", GetMovimientosPendientes)
            .WithName("Cob_GetMovimientosPendientes")
            .WithSummary("Obtiene los movimientos pendientes de pago de un crédito")
            .Produces<ApiResponseDto<List<MovimientoPendienteDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("caja-manual/pago", RegistrarPago)
            .WithName("Cob_RegistrarPago")
            .WithSummary("Registra un pago aplicándolo a los movimientos seleccionados")
            .Accepts<PagoDto>("application/json")
            .Produces<ApiResponseDto<PagoResultDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("caja-manual/pagos", GetPagos)
            .WithName("Cob_GetPagos")
            .WithSummary("Obtiene los pagos registrados de un crédito (creditoId) o de una persona (personaId)")
            .Produces<ApiResponseDto<List<PagoListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    public static async Task<IResult> GetCajaManual(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] int? personaId = null,
        [FromQuery] int? creditoId = null)
    {
        var result = await queryMediator.QueryAsync(new GetCajaManualQuery(personaId, creditoId));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetMovimientosPendientes(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int creditoId)
    {
        var result = await queryMediator.QueryAsync(new GetMovimientosPendientesQuery(creditoId));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> RegistrarPago(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] PagoDto model)
    {
        var result = await commandMediator.SendAsync(new RegistrarPagoCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetPagos(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] int? personaId = null,
        [FromQuery] int? creditoId = null)
    {
        var result = await queryMediator.QueryAsync(new GetPagosQuery(personaId, creditoId));
        return result.ToCustomMinimalApiResult();
    }
}
