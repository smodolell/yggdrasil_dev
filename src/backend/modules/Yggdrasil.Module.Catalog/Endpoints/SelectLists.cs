using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Catalog.Features.SelectLists.Queries;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.Catalog.Endpoints;

public class SelectLists : EndpointGroupBase
{
    public override string? GroupName => "cat-select-lists";
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Catalogo - SelectLists");

        group.MapGet("monedas", GetMonedaSelectList)
             .WithName("GetMonedaSelectList")
             .WithSummary("Obtiene Monedas")
             .WithDescription("Retorna una lista de las Monedas")
             .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
             .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
             .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
             .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
             .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

  
        group.MapGet("periodicidades", GetPeriodicidadSelectList)
             .WithName("GetPeriodicidadSelectList")
             .WithSummary("Obtiene Periodicidades")
             .WithDescription("Retorna una lista de las Periodicidades activas")
             .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
             .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
             .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tasas-iva", GetTasaIvaSelectList)
             .WithName("GetTasaIvaSelectList")
             .WithSummary("Obtiene Tasas IVA")
             .WithDescription("Retorna una lista de las Tasas IVA activas")
             .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
             .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
             .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);


        group.MapGet("tasas-fijas", GetTasaFijaSelectList)
             .WithName("GetTasaFijaSelectList")
             .WithSummary("Obtiene Tasas Fijas")
             .WithDescription("Retorna una lista de las Tasas Fijas activas")
             .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
             .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
             .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tasas-variables", GetTasaVariableSelectList)
             .WithName("GetTasaVariableSelectList")
             .WithSummary("Obtiene Tasas Variables")
             .WithDescription("Retorna una lista de las Tasas Variables")
             .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
             .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
             .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("bancos", GetBancoSelectList)
          .WithName("GetBancoSelectList")
          .WithSummary("Obtiene Bancos")
          .WithDescription("Retorna una lista de los Bancos activos ")
          .Produces<ApiResponseDto<List<SelectListItemDto>>>(StatusCodes.Status200OK)
          .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
          .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
    }


    public async Task<IResult> GetMonedaSelectList(
    [FromServices] IQueryMediator queryMediator,
    [FromQuery] string? searchTerm = null,
    [FromQuery] int? maxResults = null,
    CancellationToken cancellationToken = default)
    {
        var query = new GetMonedaSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        };

        var result = await queryMediator.QueryAsync(query, cancellationToken);

        return result.ToCustomMinimalApiResult();
    }

    
  
    public async Task<IResult> GetPeriodicidadSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetPeriodicidadSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetTasaIvaSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetTasaIvaSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetTasaFijaSelectList(
     [FromServices] IQueryMediator queryMediator,
     [FromQuery] string? searchTerm = null,
     [FromQuery] int? maxResults = null,
     CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetTasaFijaSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetTasaVariableSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetTasaVariableSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return result.ToCustomMinimalApiResult();
    }  
    public static async Task<IResult> GetBancoSelectList(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? maxResults = null,
        CancellationToken cancellationToken = default)
    {
        var result = await queryMediator.QueryAsync(new GetBancoSelectListQuery
        {
            SearchTerm = searchTerm,
            MaxResults = maxResults
        }, cancellationToken);
        return result.ToCustomMinimalApiResult();
    }
}