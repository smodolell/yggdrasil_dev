using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;
using Yggdrasil.Module.Cobranza.Features.CancelacionPago.DTOs;
using Yggdrasil.Module.Cobranza.Features.CancelacionPago.Commands;

namespace Yggdrasil.Module.Cobranza.Endpoints;

public class CancelacionPago : EndpointGroupBase
{
    public override string? GroupName => "cob-cancelacion-pago";
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Cobranza");

        group.MapPost("cancelacion-pago/", CancelarPago)
            .WithName("Cob_CancelarPago")
            .WithSummary("Cancela uno o más pagos aplicados, restaurando los saldos de los movimientos")
            .Accepts<CancelarPagoDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    public static async Task<IResult> CancelarPago(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] CancelarPagoDto model)
    {
        var result = await commandMediator.SendAsync(new CancelarPagoCommand(model));
        return result.ToCustomMinimalApiResult();
    }
}
