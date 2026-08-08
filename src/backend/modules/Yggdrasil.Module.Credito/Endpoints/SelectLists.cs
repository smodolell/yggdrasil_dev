using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Credito.Features.SelectLists.Queries;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.Credito.Endpoints;

public class SelectLists : EndpointGroupBase
{
    public override string? GroupName => "fi-select-lists";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Crédito - Select Lists");

        //group.MapGet("bancos", GetBancoSelectList)
        //    .WithName("CF_GetBancoSelectList")
        //    .WithSummary("Obtiene Bancos")
        //    .WithDescription("Retorna una lista de Bancos")
        //    .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
        //    .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
        //    .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
        //    .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("monedas", GetMonedaSelectList)
            .WithName("CF_GetMonedaSelectList")
            .WithSummary("Obtiene Monedas")
            .WithDescription("Retorna una lista de Monedas")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        //group.MapGet("tipos-cuenta-bancaria", GetTipoCuentaBancariaSelectList)
        //    .WithName("CF_GetTipoCuentaBancariaSelectList")
        //    .WithSummary("Obtiene Tipos de Cuenta Bancaria")
        //    .WithDescription("Retorna una lista de Tipos de Cuenta Bancaria")
        //    .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
        //    .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
        //    .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
        //    .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tipos-domicilio", GetTipoDomicilioSelectList)
            .WithName("CF_GetTipoDomicilioSelectList")
            .WithSummary("Obtiene Tipos de Domicilio")
            .WithDescription("Retorna una lista de Tipos de Domicilio")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tipos-persona", GetTipoPersonaSelectList)
            .WithName("CF_GetTipoPersonaSelectList")
            .WithSummary("Obtiene Tipos de Persona")
            .WithDescription("Retorna una lista de Tipos de Persona")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("estados-civiles", GetEdoCivilSelectList)
            .WithName("CF_GetEdoCivilSelectList")
            .WithSummary("Obtiene Estados Civiles")
            .WithDescription("Retorna una lista de Estados Civiles")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("generos", GetGeneroSelectList)
            .WithName("CF_GetGeneroSelectList")
            .WithSummary("Obtiene Géneros")
            .WithDescription("Retorna una lista de Géneros")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        //group.MapGet("estados", GetEstadoSelectList)
        //    .WithName("CF_GetEstadoSelectList")
        //    .WithSummary("Obtiene Estados")
        //    .WithDescription("Retorna una lista de Estados")
        //    .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
        //    .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
        //    .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
        //    .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        //group.MapGet("municipios", GetMunicipioSelectList)
        //    .WithName("CF_GetMunicipioSelectList")
        //    .WithSummary("Obtiene Municipios")
        //    .WithDescription("Retorna una lista de Municipios, opcionalmente filtrados por estado")
        //    .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
        //    .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
        //    .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
        //    .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("empresas", GetEmpresaSelectList)
            .WithName("CF_GetEmpresaSelectList")
            .WithSummary("Obtiene Empresas")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tipos-movimiento", GetTipoMovimientoSelectList)
            .WithName("CF_GetTipoMovimientoSelectList")
            .WithSummary("Obtiene Tipos de Movimiento activos")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tipos-pago", GetTipoPagoSelectList)
            .WithName("CF_GetTipoPagoSelectList")
            .WithSummary("Obtiene Tipos de Pago")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);


        group.MapGet("formas-pago", GetFormaPagoSelectList)
            .WithName("CF_GetFormaPagoSelectList")
            .WithSummary("Obtiene Formas de Pago")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tipos-calculo", GetTipoCalculoSelectList)
            .WithName("CF_GetTipoCalculoSelectList")
            .WithSummary("Obtiene Tipos de Calculo")
            .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }

    //public static async Task<IResult> GetBancoSelectList(
    //    [FromServices] IQueryMediator queryMediator,
    //    [FromQuery] string? searchTerm = null,
    //    [FromQuery] int? maxResults = null,
    //    CancellationToken cancellationToken = default)
    //{
    //    var result = await queryMediator.QueryAsync(new GetBancoSelectListQuery
    //    {
    //        SearchTerm = searchTerm,
    //        MaxResults = maxResults
    //    }, cancellationToken);
    //    return Result.Success(result.Value).ToCustomMinimalApiResult();
    //}

    public static async Task<IResult> GetMonedaSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetMonedaSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    //public static async Task<IResult> GetTipoCuentaBancariaSelectList(
    //    [FromServices] IQueryMediator queryMediator,
    //    [FromQuery] string? searchTerm = null,
    //    [FromQuery] int? maxResults = null,
    //    CancellationToken cancellationToken = default)
    //{
    //    var result = await queryMediator.QueryAsync(new GetTipoCuentaBancariaSelectListQuery
    //    {
    //        SearchTerm = searchTerm,
    //        MaxResults = maxResults
    //    }, cancellationToken);
    //    return Result.Success(result.Value).ToCustomMinimalApiResult();
    //}

    public static async Task<IResult> GetTipoDomicilioSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetTipoDomicilioSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetTipoPersonaSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetTipoPersonaSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetEdoCivilSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetEdoCivilSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetGeneroSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetGeneroSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    //public static async Task<IResult> GetEstadoSelectList(
    //    [FromServices] IQueryMediator queryMediator,
    //    [FromQuery] string? searchTerm = null,
    //    [FromQuery] int? maxResults = null,
    //    CancellationToken cancellationToken = default)
    //{
    //    var result = await queryMediator.QueryAsync(new GetEstadoSelectListQuery
    //    {
    //        SearchTerm = searchTerm,
    //        MaxResults = maxResults
    //    }, cancellationToken);
    //    return Result.Success(result.Value).ToCustomMinimalApiResult();
    //}

    //public static async Task<IResult> GetMunicipioSelectList(
    //    [FromServices] IQueryMediator queryMediator,
    //    [FromQuery] int? estadoId = null,
    //    [FromQuery] string? searchTerm = null,
    //    [FromQuery] int? maxResults = null,
    //    CancellationToken cancellationToken = default)
    //{
    //    var result = await queryMediator.QueryAsync(new GetMunicipioSelectListQuery
    //    {
    //        EstadoId = estadoId,
    //        SearchTerm = searchTerm,
    //        MaxResults = maxResults
    //    }, cancellationToken);
    //    return Result.Success(result.Value).ToCustomMinimalApiResult();
    //}

    public static async Task<IResult> GetEmpresaSelectList(
        [FromServices] IQueryMediator queryMediator,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetEmpresaSelectListQuery(), cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetTipoMovimientoSelectList(
        [FromServices] IQueryMediator queryMediator,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetTipoMovimientoSelectListQuery(), cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetTipoPagoSelectList(
        [FromServices] IQueryMediator queryMediator,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetTipoPagoSelectListQuery(), cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }
    public static async Task<IResult> GetFormaPagoSelectList(
        [FromServices] IQueryMediator queryMediator,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetFormaPagoSelectListQuery(), cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetTipoCalculoSelectList(
       [FromServices] IQueryMediator queryMediator,
       CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetTipoCalculoSelectListQuery(), cancellationToken);
        return Result.Success(result.Value).ToCustomMinimalApiResult();
    }
}
