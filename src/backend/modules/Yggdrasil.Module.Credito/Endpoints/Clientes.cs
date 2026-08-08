using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Credito.Features.Clientes.Commands;
using Yggdrasil.Module.Credito.Features.Clientes.DTOs;
using Yggdrasil.Module.Credito.Features.Clientes.Queries;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Yggdrasil.Module.Credito.Endpoints;

public class Clientes : EndpointGroupBase
{
    public override string? GroupName => "fi-clientes";

    public override void Map(RouteGroupBuilder groupBuilder)
    {
        var group = groupBuilder.MapGroup("/")
            .WithTags("Crédito - Clientes");

        #region Persona
        group.MapGet("persona/", GetClientes)
            .WithName("CF_GetClientes")
            .WithSummary("Obtiene clientes filtrados y paginados")
            .Produces<ApiResponseDto<PagedResultDto<PersonaListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("persona/{personaId}/perfil", GetPerfilByPersonaId)
            .WithName("GetPerfilByPersonaId")
            .WithSummary("Obtiene el perfil de un cliente por ID de persona")
            .WithDescription("Retorna la información del perfil asociado a una persona específica")
            .Produces<ApiResponseDto<PerfilDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        
        group.MapPost("persona/", CreatePersonaDefault)
            .WithName("CF_CreatePersonaDefault")
            .WithSummary("Crea una nueva persona con valores por defecto")
            .Produces<ApiResponseDto<int>>(StatusCodes.Status201Created)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto<int>>(StatusCodes.Status500InternalServerError);

        group.MapGet("persona/{id}/fisica", GetPersonaFisicaById)
            .WithName("CF_GetPersonaFisicaById")
            .WithSummary("Obtiene los datos de persona física por ID")
            .Produces<ApiResponseDto<PersonaFisicaEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("persona/{id}/fisica", SavePersonaFisica)
            .WithName("CF_SavePersonaFisica")
            .WithSummary("Guarda los datos de persona física")
            .Accepts<PersonaFisicaEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("persona/{id}/seccion-edit", GetSeccionClienteEdit)
            .WithName("CF_GetSeccionClienteEdit")
            .WithSummary("Obtiene la sección de edición del cliente por ID")
            .Produces<ApiResponseDto<ClienteEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("persona/{id}/seccion-edit", SaveSeccionClienteEdit)
            .WithName("CF_SaveSeccionClienteEdit")
            .WithSummary("Guarda la sección de edición del cliente")
            .Accepts<ClienteEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("persona/{id}", DeletePersona)
            .WithName("CF_DeletePersona")
            .WithSummary("Elimina una persona por ID")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Domicilio
        group.MapPost("domicilio/{personaId}", CreateDomicilio)
            .WithName("CF_CreateDomicilio")
            .WithSummary("Crea un nuevo domicilio para una persona")
            .Accepts<DomicilioEditDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("domicilio/{id}", UpdateDomicilio)
            .WithName("CF_UpdateDomicilio")
            .WithSummary("Actualiza un domicilio")
            .Accepts<DomicilioEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("domicilio/{id}", DeleteDomicilio)
            .WithName("CF_DeleteDomicilio")
            .WithSummary("Elimina un domicilio por ID")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region CuentaBancaria
        group.MapGet("cuenta-bancaria/", GetCuentasBancarias)
            .WithName("CF_GetCuentasBancarias")
            .WithSummary("Obtiene cuentas bancarias filtradas y paginadas")
            .Produces<ApiResponseDto<PagedResultDto<CuentaBancariaListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("cuenta-bancaria/{id}", GetCuentaBancariaById)
            .WithName("CF_GetCuentaBancariaById")
            .WithSummary("Obtiene una cuenta bancaria por ID")
            .Produces<ApiResponseDto<CuentaBancariaEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPost("cuenta-bancaria/{personaId}", CreateCuentaBancaria)
            .WithName("CF_CreateCuentaBancaria")
            .WithSummary("Crea una nueva cuenta bancaria para una persona")
            .Accepts<CuentaBancariaEditDto>("application/json")
            .Produces<ApiResponseDto>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("cuenta-bancaria/{id}", UpdateCuentaBancaria)
            .WithName("CF_UpdateCuentaBancaria")
            .WithSummary("Actualiza una cuenta bancaria")
            .Accepts<CuentaBancariaEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("cuenta-bancaria/{id}", DeleteCuentaBancaria)
            .WithName("CF_DeleteCuentaBancaria")
            .WithSummary("Elimina una cuenta bancaria por ID")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Telefono
        group.MapGet("telefono/", GetTelefonos)
            .WithName("CF_GetTelefonos")
            .WithSummary("Obtiene teléfonos filtrados y paginados")
            .Produces<ApiResponseDto<PagedResultDto<TelefonoListItemDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("telefono/{id}", GetTelefonoById)
            .WithName("CF_GetTelefonoById")
            .WithSummary("Obtiene un teléfono por ID")
            .Produces<ApiResponseDto<TelefonoEditDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapPut("telefono/{id}", UpdateTelefono)
            .WithName("CF_UpdateTelefono")
            .WithSummary("Actualiza un teléfono")
            .Accepts<TelefonoEditDto>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapDelete("telefono/{id}", DeleteTelefono)
            .WithName("CF_DeleteTelefono")
            .WithSummary("Elimina un teléfono por ID")
            .Produces(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status404NotFound)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Perfiles y Secciones

        group.MapPost("seccion-persona/sync", SyncSeccionPersona)
               .WithName("CF_SyncSeccionPersona")
               .WithSummary("Sincroniza las secciones de persona")
               .WithDescription("Sincroniza las secciones de persona, creando, actualizando y desactivando según la lista recibida")
               .Accepts<List<SeccionPersonaDto>>("application/json")
               .Produces<ApiResponseDto>(StatusCodes.Status200OK)
               .Produces<ApiResponseDto>(StatusCodes.Status400BadRequest)
               .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
               .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("perfiles/activos", GetPerfilesActivos)
            .WithName("CF_GetPerfilesActivos")
            .WithSummary("Obtiene los perfiles activos")
            .Produces<ApiResponseDto<List<PerfilDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);

        group.MapGet("secciones/by-perfil/{perfilId}", GetSeccionesByPerfilId)
            .WithName("CF_GetSeccionesByPerfilId")
            .WithSummary("Obtiene las secciones por ID de perfil")
            .Produces<ApiResponseDto<List<SeccionPersonaDto>>>(StatusCodes.Status200OK)
            .Produces<ApiResponseDto>(StatusCodes.Status401Unauthorized)
            .Produces<ApiResponseDto>(StatusCodes.Status500InternalServerError);


        #endregion
    }

    #region Persona
    public async Task<IResult> GetClientes(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortColumn = null,
        [FromQuery] bool sortDesc = false,
        [FromQuery] int? perfilId = null,
        [FromQuery] int? generoId = null,
        [FromQuery] int? edoCivilId = null,
        [FromQuery] string? lugarNacimientoId = null,
        [FromQuery] DateTime? fechaAltaClienteStart = null,
        [FromQuery] DateTime? fechaAltaClienteEnd = null)
    {
        var query = new GetClientesQuery
        {
            SearchText = q,
            Page = page,
            PageSize = pageSize,
            SortDescending = sortDesc,
            PerfilId = perfilId,
            GeneroId = generoId,
            EdoCivilId = edoCivilId,
            LugarNacimientoId = lugarNacimientoId ?? string.Empty,
            FechaAltaClienteStart = fechaAltaClienteStart,
            FechaAltaClienteEnd = fechaAltaClienteEnd
        };
        if (sortColumn != null) query.SortColumn = sortColumn;
        var result = await queryMediator.QueryAsync(query);
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetPerfilByPersonaId(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int personaId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPerfilByPersonaIdQuery(personaId);
        var result = await queryMediator.QueryAsync(query, cancellationToken);
        return result.ToCustomMinimalApiResult();
    }
    public static async Task<IResult> CreatePersonaDefault(
        [FromServices] ICommandMediator commandMediator,
        [FromBody] CreatePersonaDefaultCommand model)
    {
        var result = await commandMediator.SendAsync(model);
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetPersonaFisicaById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetPersonaFisicaByIdQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> SavePersonaFisica(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] PersonaFisicaEditDto model)
    {
        model.PersonaId = id;
        var result = await commandMediator.SendAsync(new SavePersonaFisicaCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetSeccionClienteEdit(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetSeccionClienteEditQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> SaveSeccionClienteEdit(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] ClienteEditDto model)
    {
        model.PersonaId = id;
        var result = await commandMediator.SendAsync(new SaveSeccionClienteEditCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> DeletePersona(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeletePersonaCommand(id));
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region Domicilio
    public async Task<IResult> CreateDomicilio(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int personaId,
        [FromBody] DomicilioEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateDomicilioCommand(personaId, model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateDomicilio(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] DomicilioEditDto model)
    {
        model.DomicilioId = id;
        var result = await commandMediator.SendAsync(new UpdateDomicilioCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteDomicilio(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteDomicilioCommand(id));
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region CuentaBancaria
    public async Task<IResult> GetCuentasBancarias(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] int personaId = 0,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortColumn = null,
        [FromQuery] bool sortDesc = false)
    {
        var query = new GetCuentaBancariasQuery
        {
            PersonaId = personaId,
            SearchText = q,
            Page = page,
            PageSize = pageSize,
            SortDescending = sortDesc
        };
        if (sortColumn != null) query.SortColumn = sortColumn;
        var result = await queryMediator.QueryAsync(query);
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetCuentaBancariaById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetCuentaBancariaByIdQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> CreateCuentaBancaria(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int personaId,
        [FromBody] CuentaBancariaEditDto model)
    {
        var result = await commandMediator.SendAsync(new CreateCuentaBancariaCommand(personaId, model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateCuentaBancaria(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] CuentaBancariaEditDto model)
    {
        model.CuentaBancariaId = id;
        var result = await commandMediator.SendAsync(new UpdateCuentaBancariaCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteCuentaBancaria(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteCuentaBancariaCommand { CuentaBancariaId = id });
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region Telefono
    public async Task<IResult> GetTelefonos(
        [FromServices] IQueryMediator queryMediator,
        [FromQuery] int personaId = 0,
        [FromQuery] string? q = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? sortColumn = null,
        [FromQuery] bool sortDesc = false)
    {
        var query = new GetTelefonosQuery
        {
            PersonaId = personaId,
            SearchText = q,
            Page = page,
            PageSize = pageSize,
            SortDescending = sortDesc
        };
        if (sortColumn != null) query.SortColumn = sortColumn;
        var result = await queryMediator.QueryAsync(query);
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> GetTelefonoById(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int id)
    {
        var result = await queryMediator.QueryAsync(new GetTelefonoByIdQuery(id));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> UpdateTelefono(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id,
        [FromBody] TelefonoEditDto model)
    {
        model.TelefonoId = id;
        var result = await commandMediator.SendAsync(new UpdateTelefonoCommand(model));
        return result.ToCustomMinimalApiResult();
    }

    public async Task<IResult> DeleteTelefono(
        [FromServices] ICommandMediator commandMediator,
        [FromRoute] int id)
    {
        var result = await commandMediator.SendAsync(new DeleteTelefonoCommand(id));
        return result.ToCustomMinimalApiResult();
    }
    #endregion

    #region Perfiles y Secciones

    public async Task<IResult> SyncSeccionPersona(
       [FromServices] ICommandMediator commandMediator,
       [FromBody] List<SeccionPersonaDto> model)
    {
        var result = await commandMediator.SendAsync(new SyncSeccionPersonaCommand(model));
        return result.ToCustomMinimalApiResult();
    }
    public static async Task<IResult> GetPerfilesActivos(
        [FromServices] IQueryMediator queryMediator)
    {
        var result = await queryMediator.QueryAsync(new GetPerfilesActivosQuery());
        return result.ToCustomMinimalApiResult();
    }

    public static async Task<IResult> GetSeccionesByPerfilId(
        [FromServices] IQueryMediator queryMediator,
        [FromRoute] int perfilId)
    {
        var query = new GetSeccionesByPerfilIdQuery
        {
            PerfilId = perfilId
        };
        var result = await queryMediator.QueryAsync(query);
        return result.ToCustomMinimalApiResult();
    }
    #endregion
}
