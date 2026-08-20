using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.System.Features.Configuracion.Commands;
using Yggdrasil.Module.System.Features.Configuracion.Queries;
using Yggdrasil.Module.System.Features.Configuracion.DTOs;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.System.Endpoints;

public class Configuracion : EndpointGroupBase
{
    public override string? GroupName => "configuracion";
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Sistema");

        #region Empresa
        group.MapGet("empresa/{id}", GetEmpresaById)
            .WithName("GetEmpresaById")
            .WithSummary("Obtiene una empresa por ID")
            .Produces<ApiResponseDto<EmpresaDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("empresa/", GetEmpresas)
            .WithName("GetEmpresas")
            .WithSummary("Obtiene empresas paginadas y filtradas")
            .Produces<ApiResponseDto<PagedResultDto<EmpresaListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("empresa/", CreateEmpresa)
            .WithName("CreateEmpresa")
            .WithSummary("Crea una nueva empresa")
            .Accepts<EmpresaEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("empresa/{id}", UpdateEmpresa)
            .WithName("UpdateEmpresa")
            .WithSummary("Actualiza una empresa existente")
            .Accepts<EmpresaEditDto>("application/json")
            .Produces<ApiResponseDto<bool>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("empresa/{id}", DeleteEmpresa)
            .WithName("DeleteEmpresa")
            .WithSummary("Elimina una empresa")
            .Produces<ApiResponseDto<bool>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion
    }

    #region Empresa
    public async Task<IResult> GetEmpresaById(
        [FromServices] IQueryMediator queryMediator,
        int id
    )
    {
        var result = await queryMediator.QueryAsync(new GetEmpresaByIdQuery { EmpresaId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetEmpresas(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(EmpresaDto.Id),
        [FromQuery] bool sortDescending = false)
    {
        var query = new GetEmpresasQuery
        {
            SearchText = q,
            PageSize = size,
            Page = page,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        };
        var result = await queryMediator.QueryAsync(query);
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateEmpresa(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] EmpresaEditDto model
    )
    {
        var command = new CreateEmpresaCommand
        {
            Model = model
        };

        var result = await commandMediator.SendAsync(command);
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateEmpresa(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] EmpresaEditDto model
    )
    {
        var command = new UpdateEmpresaCommand
        {
            EmpresaId = id,
            Model = model
        };

        var result = await commandMediator.SendAsync(command);
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteEmpresa(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id
    )
    {
        var command = new DeleteEmpresaCommand
        {
            EmpresaId = id
        };

        var result = await commandMediator.SendAsync(command);
        return result.ToCustomMinimalApiResult();
    }
    #endregion
}