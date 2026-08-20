using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Credito.Features.Configuracion.Perfil.Commands;
using Yggdrasil.Module.Credito.Features.Configuracion.Perfil.DTOs;
using Yggdrasil.Module.Credito.Features.Configuracion.Perfil.Queries;
using Yggdrasil.Module.Credito.Features.Configuracion.Producto.Commands;
using Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;
using Yggdrasil.Module.Credito.Features.Configuracion.Producto.Queries;
using Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.Commands;
using Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.DTOs;
using Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.Queries;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.Credito.Endpoints;

public class Configuracion : EndpointGroupBase
{
    public override string? GroupName => "fi-configuracion";
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
           .WithTags("Credito - Configuracion");

        #region TipoMovimiento
        group.MapGet("tipo-movimiento/{id}/edit", GetTipoMovimientoById)
            .WithName("GetTipoMovimientoById")
            .WithSummary("Obtiene un tipo de movimiento por ID para edición")
            .Produces<ApiResponseDto<TipoMovimientoEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized).
            Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tipo-movimiento/list", GetListTipoMovimiento)
            .WithName("GetListTipoMovimiento")
            .WithSummary("Obtiene lista paginada de tipos de movimiento")
            .Produces<ApiResponseDto<PagedResultDto<TipoMovimientoListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPatch("tipo-movimiento/{id}/activo", ChangeActivoTipoMovimiento)
            .WithName("ChangeActivoTipoMovimiento")
            .WithSummary("Activa o desactiva un tipo de movimiento")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        group.MapPost("tipo-movimiento/", CreateTipoMovimiento)
            .WithName("CreateTipoMovimiento")
            .WithSummary("Crea un nuevo tipo de movimiento")
            .Accepts<TipoMovimientoEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("tipo-movimiento/{id}", UpdateTipoMovimiento)
            .WithName("UpdateTipoMovimiento")
            .WithSummary("Actualiza un tipo de movimiento")
            .Accepts<TipoMovimientoEditDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("tipo-movimiento/{id}", DeleteTipoMovimiento)
            .WithName("DeleteTipoMovimiento")
            .WithSummary("Elimina un tipo de movimiento")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Producto
        group.MapGet("producto/", GetProductos)
            .WithName("GetProductos")
            .WithSummary("Obtiene productos filtrados")
            .Produces<ApiResponseDto<PagedResultDto<ProductoListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("producto/{id}", GetProductoById)
            .WithName("GetProductoById")
            .WithSummary("Obtiene un producto por ID para edición")
            .Produces<ApiResponseDto<ProductoEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("producto/{id}/detail", GetProductoDetail)
            .WithName("GetProductoDetail")
            .WithSummary("Obtiene el detalle de un producto por ID")
            .Produces<ApiResponseDto<ProductoDetailDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("producto/", CreateProducto)
            .WithName("CreateProducto")
            .WithSummary("Crea un nuevo producto")
            .Accepts<ProductoCreateDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("producto/{id}", UpdateProducto)
            .WithName("UpdateProducto")
            .WithSummary("Actualiza un producto")
            .Accepts<ProductoEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region CargoInicial
        group.MapGet("producto/{productoId}/cargo-inicial/", GetCargosIniciales)
            .WithName("GetCargosIniciales")
            .WithSummary("Obtiene los cargos iniciales de un producto")
            .Produces<ApiResponseDto<List<CargoInicialListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("cargo-inicial/{id}", GetCargoInicialById)
            .WithName("GetCargoInicialById")
            .WithSummary("Obtiene un cargo inicial por ID para edición")
            .Produces<ApiResponseDto<CargoInicialEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("cargo-inicial/", SaveCargoInicial)
            .WithName("SaveCargoInicial")
            .WithSummary("Crea o actualiza un cargo inicial")
            .Accepts<CargoInicialEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapDelete("cargo-inicial/{id}", DeleteCargo)
            .WithName("DeleteCargoInicial")
            .WithSummary("Elimina un cargo inicial")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region ConceptoFinanciado
        group.MapGet("producto/{productoId}/concepto-financiado/", GetConceptosFinanciados)
            .WithName("GetConceptosFinanciados")
            .WithSummary("Obtiene los conceptos financiados de un producto")
            .Produces<ApiResponseDto<List<ConceptoFinanciadoListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("concepto-financiado/{id}", GetConceptoFinanciadoById)
            .WithName("GetConceptoFinanciadoById")
            .WithSummary("Obtiene un concepto financiado por ID para edición")
            .Produces<ApiResponseDto<ConceptoFinanciadoEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("concepto-financiado/", SaveConceptoFinanciado)
            .WithName("SaveConceptoFinanciado")
            .WithSummary("Crea o actualiza un concepto financiado")
            .Accepts<ConceptoFinanciadoEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapDelete("concepto-financiado/{id}", DeleteCargo)
            .WithName("DeleteConceptoFinanciado")
            .WithSummary("Elimina un concepto financiado")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion



        #region Perfil
        group.MapGet("perfil/", GetPerfiles)
            .WithName("GetPerfiles")
            .WithSummary("Obtiene perfiles filtrados y paginados")
            .Produces<ApiResponseDto<PagedResultDto<PerfilListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("perfil/{id}", GetPerfilById)
            .WithName("GetPerfilById")
            .WithSummary("Obtiene un perfil por ID para edición (usar 0 para nuevo)")
            .Produces<ApiResponseDto<PerfilEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("perfil/", SavePerfil)
            .WithName("SavePerfil")
            .WithSummary("Crea o actualiza un perfil con sus secciones")
            .Accepts<PerfilEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapDelete("perfil/{id}", DeletePerfil)
            .WithName("DeletePerfil")
            .WithSummary("Elimina un perfil por ID")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Seccion
        group.MapGet("seccion/", GetSecciones)
            .WithName("GetSecciones")
            .WithSummary("Obtiene secciones filtradas y paginadas")
            .Produces<ApiResponseDto<PagedResultDto<SeccionListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion
    }

    #region TipoMovimiento

    public static async Task<IResult> GetTipoMovimientoById(
    [FromServices] IQueryMediator queryMediator,
    [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetTipoMovimientoByIdQuery(id));
        return result.ToCustomMinimalApiResult();
    }
    public static async Task<IResult> GetListTipoMovimiento(
    [FromServices] IQueryMediator queryMediator,
    [FromQuery] string? q = null,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string? sortColumn = null,
    [FromQuery] bool sortDesc = false,
    [FromQuery] bool? activo = null)
    {
        var query = new GetTipoMovimientosQuery
        {
            SearchText = q,
            Page = page,
            PageSize = pageSize,
            SortDescending = sortDesc,
            Activo = activo
        };
        if (sortColumn != null) query.SortColumn = sortColumn;

        var result = await queryMediator.QueryAsync(query);
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> ChangeActivoTipoMovimiento(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] ChangeActivoDto request)
    {
        var result = await commandMediator.SendAsync(new ChangeActivoTipoMovimientoCommand(id, request.Activo));
        return result.ToCustomMinimalApiResult();
    }
    public static async Task<IResult> CreateTipoMovimiento(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] TipoMovimientoEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateTipoMovimientoCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> UpdateTipoMovimiento(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] TipoMovimientoEditDto model)
    {
        model.TipoMovimientoId = id;
        var result = await commandMediator.SendAsync(new UpdateTipoMovimientoCommand(model));
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

    #region Producto
    public static async Task<IResult> GetProductos(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(ProductoListItemDto.Id),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetProductosQuery
        {
            SearchText = q
        });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetProductoById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetProductoByIdQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetProductoDetail(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetProductoDetailQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> CreateProducto(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] ProductoCreateDto model)
    {
        var result = await commandMediator.SendAsync(new CreateProductoCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> UpdateProducto(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] ProductoEditDto model)
    {
        model.ProductoId = id;
        var result = await commandMediator.SendAsync(new UpdateProductoCommand(model));
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region CargoInicial
    public static async Task<IResult> GetCargosIniciales(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int productoId)
    {
        var result = await queryMediator.QueryAsync(new GetCargoInicialesQuery { ProductoId = productoId });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetCargoInicialById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetCargoInicialByIdQuery { CargoId = id });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> SaveCargoInicial(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] CargoInicialEditDto model)
    {
        var result = await commandMediator.SendAsync(new SaveCargoInicialCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> DeleteCargo(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteCargoCommand { CargoId = id });
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region ConceptoFinanciado
    public static async Task<IResult> GetConceptosFinanciados(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int productoId)
    {
        var result = await queryMediator.QueryAsync(new GetConceptoFinanciadosQuery { ProductoId = productoId });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetConceptoFinanciadoById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetConceptoFinanciadoByIdQuery { CargoId = id });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> SaveConceptoFinanciado(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] ConceptoFinanciadoEditDto model)
    {
        var result = await commandMediator.SendAsync(new SaveConceptoFinanciadoCommand(model));
        return result.ToCustomMinimalApiResult();
    }
    #endregion



    #region Perfil
    private static async Task<IResult> GetPerfiles(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortColumn = null,
        [FromQuery] bool sortDesc = false,
        [FromQuery] bool? activo = null)
    {
        var query = new GetPerfilesQuery
        {
            SearchText = q,
            Page = page,
            PageSize = pageSize,
            SortDescending = sortDesc,
            Activo = activo
        };
        if (sortColumn != null) query.SortColumn = sortColumn;
        var result = await queryMediator.QueryAsync(query);
        return result.ToCustomMinimalApiResult();
    }

    private static async Task<IResult> GetPerfilById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetPerfilByIdQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    private static async Task<IResult> SavePerfil(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] PerfilEditDto model)
    {
        var result = await commandMediator.SendAsync(new SavePerfilCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    private static async Task<IResult> DeletePerfil(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeletePerfilCommand(id));
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region Seccion
    private static async Task<IResult> GetSecciones(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortColumn = null,
        [FromQuery] bool sortDesc = false)
    {
        var query = new GetSeccionesQuery
        {
            SearchText = q,
            Page = page,
            PageSize = pageSize,
            SortDescending = sortDesc
        };
        if (sortColumn != null) query.SortColumn = sortColumn;
        var result = await queryMediator.QueryAsync(query);
        return result.ToCustomMinimalApiResult();
    }
    #endregion
}
