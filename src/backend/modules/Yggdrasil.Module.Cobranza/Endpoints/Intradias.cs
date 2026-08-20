using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using IResult = Microsoft.AspNetCore.Http.IResult;
using Yggdrasil.Module.Cobranza.Features.Intradias.Commands;
using Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;
using Yggdrasil.Module.Cobranza.Features.Intradias.Queries;

namespace Yggdrasil.Module.Cobranza.Endpoints;

public class Intradias : EndpointGroupBase
{
    public override string? GroupName => "cob-intradias";
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Cobranza - Intradias");

        group.MapGet("intradias/credito/", GetCreditosIntraDia)
            .WithName("Cob_GetCreditosIntraDia")
            .WithSummary("Obtiene los créditos intradía paginados y ordenados")
            .Produces<ApiResponseDto<PagedResultDto<CreditoIntraDiaListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("intradias/credito/{id}", GetCreditoIntraDiaById)
            .WithName("Cob_GetCreditoIntraDiaById")
            .WithSummary("Obtiene un crédito intradía por ID")
            .Produces<ApiResponseDto<CreditoIntradiaEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("intradias/credito/{id}/detail", GetCreditoIntraDiaDetail)
            .WithName("Cob_GetCreditoIntraDiaDetail")
            .WithSummary("Obtiene el detalle de un crédito intradía con sus movimientos e intereses acumulados")
            .Produces<ApiResponseDto<CreditoIntraDiaDetailDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("intradias/credito/{id}", CapturarCredito)
            .WithName("Cob_CapturarCreditoIntraDia")
            .WithSummary("Crea o actualiza un crédito intradía")
            .Accepts<CreditoIntradiaEditDto>("application/json")
            .Produces<ApiResponseDto<Guid>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("intradias/devengar", Devengar)
            .WithName("Cob_DevengarCreditoIntraDia")
            .WithSummary("Devenga el interés acumulado de un crédito intradía a la fecha indicada")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("intradias/disposicion", NewDisposicion)
            .WithName("Cob_NewDisposicionIntraDia")
            .WithSummary("Registra una disposición de capital sobre un crédito intradía")
            .Accepts<NewDisposicionDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("intradias/pago-capital", PagoCapital)
            .WithName("Cob_PagoCapitalIntraDia")
            .WithSummary("Aplica un pago a capital sobre un crédito intradía")
            .Accepts<PagoCapitalDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("intradias/prepago", Prepago)
            .WithName("Cob_PrepagoIntraDia")
            .WithSummary("Aplica un prepago (interés e IVA devengados y, en su caso, capital) sobre un crédito intradía")
            .Accepts<PrepagoDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("intradias/pago-interes", PagoInteres)
            .WithName("Cob_PagoInteresIntraDia")
            .WithSummary("Aplica un pago a intereses e IVA acumulados sobre un crédito intradía")
            .Accepts<PagoInteresDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("intradias/liquidacion", Liquidacion)
            .WithName("Cob_LiquidacionIntraDia")
            .WithSummary("Liquida el saldo total (capital, interés e IVA) de un crédito intradía")
            .Accepts<LiquidacionDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("intradias/credito/{id}/movimientos", EliminarMovimientos)
            .WithName("Cob_EliminarMovimientosIntraDia")
            .WithSummary("Elimina los movimientos y el interés acumulado registrados de un crédito intradía")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("intradias/credito/{id}/nueva-disposicion", GetNewDisposicion)
            .WithName("Cob_GetNewDisposicionIntraDia")
            .WithSummary("Calcula los valores por defecto (fecha) para una nueva disposición sobre un crédito intradía")
            .Produces<ApiResponseDto<NewDisposicionDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("intradias/credito/{id}/pago-capital", GetPagoCapital)
            .WithName("Cob_GetPagoCapitalIntraDia")
            .WithSummary("Calcula los valores por defecto (fecha) para un pago a capital sobre un crédito intradía")
            .Produces<ApiResponseDto<PagoCapitalDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("intradias/credito/{id}/pago-interes", GetPagoInteres)
            .WithName("Cob_GetPagoInteresIntraDia")
            .WithSummary("Calcula los valores por defecto (fecha) para un pago a intereses sobre un crédito intradía")
            .Produces<ApiResponseDto<PagoInteresDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("intradias/credito/{id}/liquidacion", GetLiquidacion)
            .WithName("Cob_GetLiquidacionIntraDia")
            .WithSummary("Calcula los valores por defecto (fecha) para la liquidación de un crédito intradía")
            .Produces<ApiResponseDto<LiquidacionDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    public static async Task<IResult> GetCreditosIntraDia(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortColumn = null,
        [FromQuery] bool sortDesc = true,
        [FromQuery] DateTime? fechaPrimeraRentaStart = null,
        [FromQuery] DateTime? fechaPrimeraRentaEnd = null)
    {
        var query = new GetCreditosIntraDiaQuery
        {
            Page = page,
            PageSize = pageSize,
            SortDescending = sortDesc,
            FechaPrimeraRentaStart = fechaPrimeraRentaStart,
            FechaPrimeraRentaEnd = fechaPrimeraRentaEnd
        };
        if (sortColumn != null) query.SortColumn = sortColumn;
        var result = await queryMediator.QueryAsync(query);
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetCreditoIntraDiaById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] Guid id)
    {
        var result = await queryMediator.QueryAsync(new GetCreditoIntraDiaByIdQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetCreditoIntraDiaDetail(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] Guid id)
    {
        var result = await queryMediator.QueryAsync(new GetCreditoIntraDiaDetailQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> CapturarCredito(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] Guid id,
        [FromBody] CreditoIntradiaEditDto model)
    {
        var result = await commandMediator.SendAsync(new CapturarCreditoCommand(id, model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> Devengar(
        [FromServices] ICommandMediator commandMediator,
        [FromQuery] Guid creditoId,
        [FromQuery] DateTime? fechaCalculo = null)
    {
        var result = await commandMediator.SendAsync(new DevengarCommand(creditoId, fechaCalculo ?? DateTime.Now));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> NewDisposicion(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] NewDisposicionDto model)
    {
        var result = await commandMediator.SendAsync(new NewDisposicionCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> PagoCapital(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] PagoCapitalDto model)
    {
        var result = await commandMediator.SendAsync(new PagoCapitalCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> Prepago(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] PrepagoDto model)
    {
        var result = await commandMediator.SendAsync(new PrepagoCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> PagoInteres(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] PagoInteresDto model)
    {
        var result = await commandMediator.SendAsync(new PagoInteresCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> Liquidacion(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] LiquidacionDto model)
    {
        var result = await commandMediator.SendAsync(new LiquidacionCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> EliminarMovimientos(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] Guid id)
    {
        var result = await commandMediator.SendAsync(new EliminarMovimientosCommand(id));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetNewDisposicion(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] Guid id)
    {
        var result = await queryMediator.QueryAsync(new GetNewDisposicionQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetPagoCapital(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] Guid id)
    {
        var result = await queryMediator.QueryAsync(new GetPagoCapitalQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetPagoInteres(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] Guid id)
    {
        var result = await queryMediator.QueryAsync(new GetPagoInteresQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetLiquidacion(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] Guid id)
    {
        var result = await queryMediator.QueryAsync(new GetLiquidacionQuery(id));
        return result.ToCustomMinimalApiResult();
    }
}
