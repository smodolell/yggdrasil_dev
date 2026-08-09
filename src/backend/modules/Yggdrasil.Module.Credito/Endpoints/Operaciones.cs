using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Credito.Features.Clientes.DTOs;
using Yggdrasil.Module.Credito.Features.Clientes.Queries;
using Yggdrasil.Module.Credito.Features.Creditos.DTOs;
using Yggdrasil.Module.Credito.Features.Creditos.Queries;
using Yggdrasil.Module.Credito.Features.Operaciones.ActivarCredito.Commands;
using Yggdrasil.Module.Credito.Features.Operaciones.CapturaCredito.Commands;
using Yggdrasil.Module.Credito.Features.Operaciones.CapturaCredito.DTOs;
using Yggdrasil.Module.Credito.Features.Operaciones.CapturaCredito.Queries;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.Credito.Endpoints;

public class Operaciones : EndpointGroupBase
{
    public override string? GroupName => "fi-operaciones";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Crédito - Operaciones");

     

        group.MapGet("credito/new/{personaId}", GetNewCredito)
            .WithName("GetNewCredito")
            .WithSummary("Obtiene la plantilla de nuevo crédito para una persona")
            .Produces<ApiResponseDto<CreditoEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("credito/{id}", GetCreditoById)
            .WithName("GetCreditoById")
            .WithSummary("Obtiene los datos de un crédito por ID")
            .Produces<ApiResponseDto<CreditoEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("credito/", CreateCredito)
            .WithName("CreateCredito")
            .WithSummary("Crea un nuevo crédito")
            .Accepts<CreditoEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("credito/{id}", UpdateCredito)
            .WithName("CF_UpdateCredito")
            .WithSummary("Actualiza los datos de un crédito")
            .Accepts<CreditoEditDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("credito/{id}/activar", ActivarCredito)
            .WithName("CF_ActivarCredito")
            .WithSummary("Activa un crédito mediante el procedimiento almacenado")
            .Produces<ApiResponseDto<string>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

  

    public static async Task<IResult> GetNewCredito(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int personaId)
    {
        var result = await queryMediator.QueryAsync(new GetNewCreditoQuery { PersonaId = personaId });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetCreditoById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetCreditoByIdQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> CreateCredito(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] CreditoEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateCreditoCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> UpdateCredito(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] CreditoEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateCreditoCommand(id, model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> ActivarCredito(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new ActivarCreditoCommand(id));
        return result.ToCustomMinimalApiResult();
    }








}
