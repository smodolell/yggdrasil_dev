using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Credito.CS.Features.Configuracion.Commands;
using Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Configuracion.Queries;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.Credito.CS.Endpoints;

public class Configuracion : EndpointGroupBase
{
    public override string? GroupName => "cs-configuracion";
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("CreditoCS - Configuracion");

        
        #region TipoMovimiento
        group.MapGet("tipo-movimiento/{id}", GetTipoMovimientoById)
            .WithName("GetCSTipoMovimientoById")
            .WithSummary("Obtiene un tipo de movimiento de crédito simple por ID")
            .Produces<ApiResponseDto<TipoMovimientoCsEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tipo-movimiento/", GetTipoMovimientos)
            .WithName("GetCSTipoMovimientos")
            .WithSummary("Obtiene tipos de movimiento de crédito simple paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<TipoMovimientoCsListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("tipo-movimiento/", CreateTipoMovimiento)
            .WithName("CreateCSTipoMovimiento")
            .WithSummary("Crea un nuevo tipo de movimiento de crédito simple")
            .Accepts<TipoMovimientoCsEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("tipo-movimiento/{id}", UpdateTipoMovimiento)
            .WithName("UpdateCSTipoMovimiento")
            .WithSummary("Actualiza un tipo de movimiento de crédito simple")
            .Accepts<TipoMovimientoCsEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("tipo-movimiento/{id}", DeleteTipoMovimiento)
            .WithName("DeleteCSTipoMovimiento")
            .WithSummary("Elimina un tipo de movimiento de crédito simple")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region TipoCredito
        group.MapGet("tipo-credito/{id}", GetTipoCreditoById)
            .WithName("GetCSTipoCreditoById")
            .WithSummary("Obtiene un tipo de crédito simple por ID")
            .Produces<ApiResponseDto<TipoCreditoCsEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tipo-credito/", GetTipoCreditos)
            .WithName("GetCSTipoCreditos")
            .WithSummary("Obtiene tipos de crédito simple paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<TipoCreditoListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("tipo-credito/", CreateTipoCredito)
            .WithName("CreateCSTipoCredito")
            .WithSummary("Crea un nuevo tipo de crédito simple")
            .Accepts<TipoCreditoCsEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("tipo-credito/{id}", UpdateTipoCredito)
            .WithName("UpdateCSTipoCredito")
            .WithSummary("Actualiza un tipo de crédito simple")
            .Accepts<TipoCreditoCsEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("tipo-credito/{id}", DeleteTipoCredito)
            .WithName("DeleteCSTipoCredito")
            .WithSummary("Elimina un tipo de crédito simple")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region MetodoArmotizacion
        group.MapPost("metodo-armortizacion/sync", SyncMetodoArmotizacion)
            .WithName("SyncCSMetodoArmotizacion")
            .WithSummary("Sincroniza los métodos de amortización")
            .WithDescription("Sincroniza el catálogo de métodos de amortización con las estrategias registradas en el sistema (DI), agregando, actualizando y desactivando según corresponda")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion
    }



    #region TipoMovimiento
    public static async Task<IResult> GetTipoMovimientoById(
        [FromServices] IQueryMediator queryMediator,
        int id)
    {
        var result = await queryMediator.QueryAsync(new GetTipoMovimientoByIdQuery { TipoMovimientoId = id });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetTipoMovimientos(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(TipoMovimientoCsListItemDto.Id),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetTipoMovimientosQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> CreateTipoMovimiento(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] TipoMovimientoCsEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateTipoMovimientoCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> UpdateTipoMovimiento(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] TipoMovimientoCsEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateTipoMovimientoCommand { TipoMovimientoId = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> DeleteTipoMovimiento(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteTipoMovimientoCommand { TipoMovimientoId = id });
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region TipoCredito
    public static async Task<IResult> GetTipoCreditoById(
        [FromServices] IQueryMediator queryMediator,
        int id)
    {
        var result = await queryMediator.QueryAsync(new GetTipoCreditoByIdQuery { TipoCreditoId = id });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetTipoCreditos(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(TipoCreditoListItemDto.Id),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetTipoCreditosQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> CreateTipoCredito(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] TipoCreditoCsEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateTipoCreditoCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> UpdateTipoCredito(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] TipoCreditoCsEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateTipoCreditoCommand { TipoCreditoId = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> DeleteTipoCredito(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteTipoCreditoCommand { TipoCreditoId = id });
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region MetodoArmotizacion
    public static async Task<IResult> SyncMetodoArmotizacion(
        [FromServices] ICommandMediator commandMediator)
    {
        var result = await commandMediator.SendAsync(new SyncMetodoArmotizacionCommand());
        return result.ToCustomMinimalApiResult();
    }
    #endregion
}
