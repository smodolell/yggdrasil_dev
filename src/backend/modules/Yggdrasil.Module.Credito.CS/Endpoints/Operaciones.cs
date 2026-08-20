using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Credito.CS.Features.Operaciones.Commands;
using Yggdrasil.Module.Credito.CS.Features.Operaciones.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Operaciones.Queries;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.Credito.CS.Endpoints;

public class Operaciones : EndpointGroupBase
{
    public override string? GroupName => "cs-operacion";
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("CreditoCS - Operacion");

        group.MapGet("credito/{id}", GetCreditoEditById)
            .WithName("CSGetCreditoEditById")
            .WithSummary("Obtiene los datos de un crédito simple para edición")
            .Produces<ApiResponseDto<CreditoCSEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("credito/", CapturarCredito)
            .WithName("CSCapturarCredito")
            .WithSummary("Captura un nuevo crédito simple")
            .Accepts<CreditoCSEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("credito/{id}", ActualizarCredito)
            .WithName("CSActualizarCredito")
            .WithSummary("Actualiza un crédito simple existente")
            .Accepts<CreditoCSEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPost("credito/{id}/activar", ActivarCredito)
            .WithName("CSActivarCredito")
            .WithSummary("Activa un crédito simple")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    public static async Task<IResult> GetCreditoEditById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetCreditoEditByIdQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> CapturarCredito(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] CreditoCSEditDto model)
    {
        var result = await commandMediator.SendAsync(new CapturarCreditoCommand(0, model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> ActualizarCredito(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] CreditoCSEditDto model)
    {
        var result = await commandMediator.SendAsync(new CapturarCreditoCommand(id, model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> ActivarCredito(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromQuery] DateTime? fechaActivacion = null)
    {
        var result = await commandMediator.SendAsync(new ActivarCreditoCommand(id, fechaActivacion));
        return result.ToCustomMinimalApiResult();
    }
}