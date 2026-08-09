using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Catalog.Features.Catalogos.Commands;
using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;
using Yggdrasil.Module.Catalog.Features.Catalogos.Queries;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.Catalog.Endpoints;

public class Catalogos : EndpointGroupBase
{
    public override string? GroupName => "cat-catalogos";
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Catalogo - Catalogos");

        #region Banco
        group.MapGet("banco/{id}", GetBancoById)
            .WithName("GetBancoById")
            .WithSummary("Obtiene un banco por ID")
            .Produces<ApiResponseDto<BancoEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("banco/", GetBancos)
            .WithSummary("Obtiene bancos paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<BancoListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("banco/", CreateBanco)
            .WithName("CreateBanco")
            .WithSummary("Crea un nuevo banco")
            .Accepts<BancoEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("banco/{id}", UpdateBanco)
            .WithName("UpdateBanco")
            .WithSummary("Actualiza un banco")
            .Accepts<BancoEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("banco/{id}", DeleteBanco)
            .WithName("DeleteBanco")
            .WithSummary("Elimina un banco")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Moneda
        group.MapGet("moneda/{id}", GetMonedaById)
            .WithName("GetMonedaById")
            .WithSummary("Obtiene una moneda por ID")
            .Produces<ApiResponseDto<MonedaEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("moneda/", GetMonedas)
            .WithSummary("Obtiene monedas paginadas y filtradas")
            .Produces<ApiResponseDto<PagedResultDto<MonedaListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("moneda/", CreateMoneda)
            .WithName("CreateMoneda")
            .WithSummary("Crea una nueva moneda")
            .Accepts<MonedaEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("moneda/{id}", UpdateMoneda)
            .WithName("UpdateMoneda")
            .WithSummary("Actualiza una moneda")
            .Accepts<MonedaEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("moneda/{id}", DeleteMoneda)
            .WithName("DeleteMoneda")
            .WithSummary("Elimina una moneda")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Periodicidad
        group.MapGet("periodicidad/{id}", GetPeriodicidadById)
            .WithName("GetPeriodicidadById")
            .WithSummary("Obtiene una periodicidad por ID")
            .Produces<ApiResponseDto<PeriodicidadEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("periodicidad/", GetPeriodicidades)
            .WithSummary("Obtiene periodicidades paginadas y filtradas")
            .Produces<ApiResponseDto<PagedResultDto<PeriodicidadListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("periodicidad/", CreatePeriodicidad)
            .WithName("CreatePeriodicidad")
            .WithSummary("Crea una nueva periodicidad")
            .Accepts<PeriodicidadEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("periodicidad/{id}", UpdatePeriodicidad)
            .WithName("UpdatePeriodicidad")
            .WithSummary("Actualiza una periodicidad")
            .Accepts<PeriodicidadEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("periodicidad/{id}", DeletePeriodicidad)
            .WithName("DeletePeriodicidad")
            .WithSummary("Elimina una periodicidad")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Plazo
        group.MapGet("plazo/{id}", GetPlazoById)
            .WithName("GetPlazoById")
            .WithSummary("Obtiene un plazo por ID")
            .Produces<ApiResponseDto<PlazoEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("plazo/", GetPlazos)
            .WithSummary("Obtiene plazos paginados y filtrados")
            .Produces<ApiResponseDto<PagedResultDto<PlazoListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("plazo/", CreatePlazo)
            .WithName("CreatePlazo")
            .WithSummary("Crea un nuevo plazo")
            .Accepts<PlazoEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("plazo/{id}", UpdatePlazo)
            .WithName("UpdatePlazo")
            .WithSummary("Actualiza un plazo")
            .Accepts<PlazoEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("plazo/{id}", DeletePlazo)
            .WithName("DeletePlazo")
            .WithSummary("Elimina un plazo")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Tasa
        group.MapGet("tasa/{id}", GetTasaById)
            .WithName("GetTasaById")
            .WithSummary("Obtiene una tasa por ID")
            .Produces<ApiResponseDto<TasaFijaEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tasa/", GetTasas)
            .WithSummary("Obtiene tasas paginadas y filtradas")
            .Produces<ApiResponseDto<PagedResultDto<TasaListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("tasa/", CreateTasa)
            .WithName("CreateTasa")
            .WithSummary("Crea una nueva tasa")
            .Accepts<TasaFijaEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("tasa/{id}", UpdateTasa)
            .WithName("UpdateTasa")
            .WithSummary("Actualiza una tasa")
            .Accepts<TasaFijaEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("tasa/{id}", DeleteTasa)
            .WithName("DeleteTasa")
            .WithSummary("Elimina una tasa")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPatch("tasa/{id}/active", ChangeActiveTasa)
            .WithName("ChangeActiveTasa")
            .WithSummary("Cambia el estado activo/inactivo de una tasa")
            .Accepts<ChangeActiveTasaDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region TasaIva
        group.MapGet("tasa-iva/{id}", GetTasaIvaById)
            .WithName("GetTasaIvaById")
            .WithSummary("Obtiene una tasa IVA por ID")
            .Produces<ApiResponseDto<TasaIvaEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tasa-iva/", GetTasasIva)
            .WithSummary("Obtiene tasas IVA paginadas y filtradas")
            .Produces<ApiResponseDto<PagedResultDto<TasaIvaListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("tasa-iva/", CreateTasaIva)
            .WithName("CreateTasaIva")
            .WithSummary("Crea una nueva tasa IVA")
            .Accepts<TasaIvaEditDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("tasa-iva/{id}", UpdateTasaIva)
            .WithName("UpdateTasaIva")
            .WithSummary("Actualiza una tasa IVA")
            .Accepts<TasaIvaEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("tasa-iva/{id}", DeleteTasaIva)
            .WithName("DeleteTasaIva")
            .WithSummary("Elimina una tasa IVA")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPatch("tasa-iva/{id}/active", ChangeActiveTasaIva)
            .WithName("ChangeActiveTasaIva")
            .WithSummary("Cambia el estado activo/inactivo de una tasa IVA")
            .Accepts<ChangeActiveTasaDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region TasaVariable
        group.MapGet("tasa-variable/{id}", GetTasaVariableById)
            .WithName("GetTasaVariableById")
            .WithSummary("Obtiene una tasa variable por ID")
            .Produces<ApiResponseDto<TasaVariableDetalleDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("tasa-variable/", GetTasasVariables)
            .WithSummary("Obtiene tasas variables paginadas y filtradas")
            .Produces<ApiResponseDto<PagedResultDto<TasaVariableListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("tasa-variable/", CreateTasaVariable)
            .WithName("CreateTasaVariable")
            .WithSummary("Crea una nueva tasa variable")
            .Accepts<TasaVariableDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("tasa-variable/{id}", UpdateTasaVariable)
            .WithName("UpdateTasaVariable")
            .WithSummary("Actualiza una tasa variable")
            .Accepts<TasaVariableDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("tasa-variable/{tasaId}/valor/", CreateTasaValor)
            .WithName("CreateTasaValor")
            .WithSummary("Agrega un valor a una tasa variable")
            .Accepts<TasaValorDto>("application/json")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapPut("tasa-variable/{tasaId}/valor/{id}", UpdateTasaValor)
            .WithName("UpdateTasaValor")
            .WithSummary("Actualiza un valor de tasa variable")
            .Accepts<TasaValorDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion
    }

    #region Banco
    public static async Task<IResult> GetBancoById(
        [FromServices] IQueryMediator queryMediator,
        int id)
    {
        var result = await queryMediator.QueryAsync(new GetBancoByIdQuery { BancoId = id });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetBancos(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(BancoListItemDto.Id),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetBancosQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> CreateBanco(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] BancoEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateBancoCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> UpdateBanco(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] BancoEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateBancoCommand { BancoId = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> DeleteBanco(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteBancoCommand { BancoId = id });
        return result.ToCustomMinimalApiResult();
    }
    #endregion


    #region Moneda
    public async Task<IResult> GetMonedaById(
        [FromServices] IQueryMediator queryMediator,
        int id)
    {
        var result = await queryMediator.QueryAsync(new GetMonedaByIdQuery { MonedaId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetMonedas(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(MonedaListItemDto.Id),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetMonedasQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateMoneda(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] MonedaEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateMonedaCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateMoneda(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] MonedaEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateMonedaCommand { MonedaId = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteMoneda(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteMonedaCommand { MonedaId = id });
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region Periodicidad
    public async Task<IResult> GetPeriodicidadById(
        [FromServices] IQueryMediator queryMediator,
        int id)
    {
        var result = await queryMediator.QueryAsync(new GetPeriodicidadByIdQuery { PeriodicidadId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetPeriodicidades(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(PeriodicidadListItemDto.Id),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetPeriodicidadesQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreatePeriodicidad(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] PeriodicidadEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreatePeriodicidadCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdatePeriodicidad(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] PeriodicidadEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdatePeriodicidadCommand { PeriodicidadId = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeletePeriodicidad(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeletePeriodicidadCommand { PeriodicidadId = id });
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region Plazo
    public async Task<IResult> GetPlazoById(
        [FromServices] IQueryMediator queryMediator,
        int id)
    {
        var result = await queryMediator.QueryAsync(new GetPlazoByIdQuery { PlazoId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetPlazos(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] int? valorPlazo,
        [FromQuery] bool? activo,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(PlazoListItemDto.Id),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetPlazosQuery
        {
            ValorPlazo = valorPlazo,
            Activo = activo,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreatePlazo(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] PlazoEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreatePlazoCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdatePlazo(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] PlazoEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdatePlazoCommand { PlazoId = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeletePlazo(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeletePlazoCommand { PlazoId = id });
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region Tasa
    public async Task<IResult> GetTasaById(
        [FromServices] IQueryMediator queryMediator,
        int id)
    {
        var result = await queryMediator.QueryAsync(new GetTasaFijaByIdQuery { TasaId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetTasas(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] decimal? valueMin = null,
        [FromQuery] decimal? valueMax = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(TasaListItemDto.Id),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetTasasFijasQuery
        {
            ValueMin = valueMin,
            ValueMax = valueMax,
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateTasa(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] TasaFijaEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateTasaFijaCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateTasa(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] TasaFijaEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateTasaFijaCommand { TasaId = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteTasa(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteTasaCommand { TasaId = id });
        return result.ToCustomMinimalApiResult();
    }
    public async Task<IResult> ChangeActiveTasa(
     [FromServices] ICommandMediator commandMediator,
     [FromRoute] int id,
     [FromBody] ChangeActiveTasaDto request)
    {
        var result = await commandMediator.SendAsync(new ChangeActiveTasaCommand
        {
            TasaId = id,
            Active = request.Active
        });
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region TasaIva
    public async Task<IResult> GetTasaIvaById(
        [FromServices] IQueryMediator queryMediator,
        int id)
    {
        var result = await queryMediator.QueryAsync(new GetTasaIvaByIdQuery { TasaIvaId = id });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetTasasIva(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(TasaIvaListItemDto.Id),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetTasasIvaQuery
        {
            SearchText = q,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateTasaIva(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] TasaIvaEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateTasaIvaCommand { Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateTasaIva(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] TasaIvaEditDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateTasaIvaCommand { TasaIvaId = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteTasaIva(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteTasaIvaCommand { TasaIvaId = id });
        return result.ToCustomMinimalApiResult();
    }
    public async Task<IResult> ChangeActiveTasaIva(
   [FromServices] ICommandMediator commandMediator,
   [FromRoute] int id,
   [FromBody] ChangeActiveTasaDto request)
    {
        var result = await commandMediator.SendAsync(new ChangeActiveTasaIvaCommand
        {
            TasaIvaId = id,
            Active = request.Active
        });
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region TasaVariable
    public async Task<IResult> GetTasaVariableById(
        [FromServices] IQueryMediator queryMediator,
        int id)
    {
        var result = await queryMediator.QueryAsync(new GetTasaVariableByIdQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetTasasVariables(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] bool? activa = null,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string sortColumn = nameof(TasaVariableListItemDto.NomTasa),
        [FromQuery] bool sortDescending = false)
    {
        var result = await queryMediator.QueryAsync(new GetTasasVariablesQuery
        {
            SearchText = q,
            Activa = activa,
            Page = page,
            PageSize = size,
            SortColumn = sortColumn,
            SortDescending = sortDescending
        });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateTasaVariable(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] TasaVariableDto model)
    {
        var result = await commandMediator.SendAsync(new CreateTasaVariableCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateTasaVariable(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] TasaVariableDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateTasaVariableCommand { Id = id, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateTasaValor(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int tasaId,
        [FromBody] TasaValorDto model)
    {
        var result = await commandMediator.SendAsync(new CreateTasaValorCommand { TasaId = tasaId, Model = model });
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateTasaValor(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int tasaId,
        [FromRoute] int id,
        [FromBody] TasaValorDto model)
    {
        var result = await commandMediator.SendAsync(new UpdateTasaValorCommand(id, model));
        return result.ToCustomMinimalApiResult();
    }
    #endregion

}
