using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Report.Features.Reportes.Commands;
using Yggdrasil.Module.Report.Features.Reportes.Queries;
using Yggdrasil.Module.Report.Features.Reportes.DTOs;
using IResult = Microsoft.AspNetCore.Http.IResult;
using Yggdrasil.Common.DTOs;

namespace Yggdrasil.Module.Report.Endpoints;

public class Reportes : EndpointGroupBase
{
    public override string? GroupName => "reportes";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .RequireAuthorization()
            .WithTags("Reportes");

        #region Reporte
        group.MapGet("reporte/{id:int}", GetReporteById)
            .WithName("GetReporteById")
            .WithSummary("Obtiene un reporte por ID")
            .Produces<ApiResponseDto<ReporteEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("reporte/", GetReportes)
             .WithName("GetReportes")
            .WithSummary("Obtiene reportes paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<ReporteListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("reporte/search", SearchReportes)
            .WithName("SearchReportes")
            .WithSummary("Busca reportes para lista de selección")
            .Produces<ApiResponseDto<List<SelectReporteDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("reporte/", CreateReporte)
            .WithName("CreateReporte")
            .WithSummary("Crea un nuevo reporte")
            .Accepts<ReporteEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("reporte/{id:int}", UpdateReporte)
            .WithName("UpdateReporte")
            .WithSummary("Actualiza un reporte")
            .Accepts<ReporteEditDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("reporte/{id:int}", DeleteReporte)
            .WithName("DeleteReporte")
            .WithSummary("Elimina un reporte y sus parámetros")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("reporte/{id:int}/configuracion", GetReporteConfiguracion)
            .WithName("GetReporteConfiguracion")
            .WithSummary("Obtiene la configuración de parámetros de un reporte para su ejecución")
            .Produces<ApiResponseDto<ReporteExecuteDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("reporte/ejecutar", EjecutarReporte)
            .WithName("EjecutarReporte")
            .WithSummary("Ejecuta un reporte y retorna el archivo generado (Excel o Texto)")
            .Accepts<ReporteExecuteDto>("application/json")
            .Produces<ApiResponseDto<ReporteExportDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("reporte/automatizar", AutomatizarReporte)
            .WithName("AutomatizarReporte")
            .WithSummary("Registra una automatización de reporte")
            .Accepts<ReporteExecuteDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Archivo
        group.MapGet("archivo/", GetArchivos)
             .WithName("GetArchivos")
            .WithSummary("Obtiene archivos generados paginados, filtrable por reporte")
            .Produces<ApiResponseDto<PagedResultDto<ArchivoListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("archivo/{id:guid}/download", DownloadArchivo)
            .WithName("DownloadArchivo")
            .WithSummary("Descarga un archivo generado por ID")
            .Produces<ApiResponseDto<ReporteExportDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("archivo/{id:guid}", DeleteArchivo)
            .WithName("DeleteArchivo")
            .WithSummary("Elimina un archivo del disco y su registro")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Parametro
        group.MapGet("parametro/{id:guid}", GetParametroById)
            .WithName("GetParametroById")
            .WithSummary("Obtiene un parámetro por ID")
            .Produces<ApiResponseDto<ParametroEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("parametro/", GetParametros)
            .WithName("GetParametros")
            .WithSummary("Obtiene parámetros filtrados por reporte")
            .Produces<ApiResponseDto<List<ParametroListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("parametro/{id:guid}", UpdateParametro)
            .WithName("UpdateParametro")
            .WithSummary("Actualiza la configuración de un parámetro")
            .Accepts<ParametroEditDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion
    }

    #region Reporte
    public async Task<IResult> GetReporteById(
        [FromServices] IQueryMediator queryMediator,
        int id)
    {
        var result = await queryMediator.QueryAsync(new GetReporteByIdQuery { ReporteId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetReportes(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(ReporteListItemDto.NomReporte),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetReportesQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> SearchReportes(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null)
    {
        var result = await queryMediator.QueryAsync(new SearchReportesQuery { SearchText = q });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateReporte(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] ReporteEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateReporteCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateReporte(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] ReporteEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateReporteCommand { ReporteId = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteReporte(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteReporteCommand { ReporteId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetReporteConfiguracion(
        [FromServices] IQueryMediator queryMediator,
        int id)
    {
        var result = await queryMediator.QueryAsync(new GetReporteConfiguracionQuery { ReporteId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> EjecutarReporte(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] ReporteExecuteDto model,
        [FromQuery] bool guardarArchivo = false)
    {
        var result = await commandMediator.SendAsync(new EjecutarReporteCommand { Model = model, GuardarArchivo = guardarArchivo });
        if (!result.IsSuccess)
            return result.ToCustomMinimalApiResult();

        var export = result.Value;
        return Results.File(export.Data, export.ContentType, export.FileName);
    }

    public async Task<IResult> AutomatizarReporte(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] ReporteExecuteDto model)
    {
        var result = await commandMediator.SendAsync(new AutomatizarReporteCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region Parametro
    public async Task<IResult> GetParametroById(
        [FromServices] IQueryMediator queryMediator,
        Guid id)
    {
        var result = await queryMediator.QueryAsync(new GetParametroByIdQuery { ParametroId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetParametros(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] int? reporteId = null,
        [FromQuery] string? q = null)
    {
        var result = await queryMediator.QueryAsync(new GetParametrosQuery
        {
            ReporteId = reporteId,
            SearchText = q
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateParametro(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] Guid id,
        [FromBody] ParametroEditDto model)
    {
        model.ParametroId = id;
        var result = await commandMediator.SendAsync(new UpdateParametroCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region Archivo
    public async Task<IResult> GetArchivos(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] int? reporteId = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(ArchivoListItemDto.FechaCreacion),
        [FromQuery] bool sortDescending = true)
    {
        var result = await queryMediator.QueryAsync(new GetArchivosQuery
        {
            ReporteId = reporteId,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DownloadArchivo(
        [FromServices] IQueryMediator queryMediator,
        Guid id)
    {
        var result = await queryMediator.QueryAsync(new DownloadArchivoQuery { ArchivoId = id });
        if (!result.IsSuccess)
            return result.ToCustomMinimalApiResult();

        var export = result.Value;
        return Results.File(export.Data, export.ContentType, export.FileName);
    }

    public async Task<IResult> DeleteArchivo(
        [FromServices] ICommandMediator commandMediator,
        Guid id)
    {
        var result = await commandMediator.SendAsync(new DeleteArchivoCommand { ArchivoId = id });
        return result.ToCustomMinimalApiResult();
    }
    #endregion
}
