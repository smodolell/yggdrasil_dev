using Yggdrasil.Module.Credito.UI.Services.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.UI.Services.Clientes;

public interface IClientesApi
{
    #region Persona

    /// <summary>
    /// Obtiene clientes filtrados y paginados
    /// </summary>
    [Get("/api/fi-clientes/persona/")]
    Task<ApiResponseDto<PagedResultDto<PersonaListItemDto>>> GetClientes(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int pageSize = 10,
        [Query] string? sortColumn = null,
        [Query] bool sortDesc = false,
        [Query] int? perfilId = null,
        [Query] int? generoId = null,
        [Query] int? edoCivilId = null,
        [Query] string? lugarNacimientoId = null,
        [Query] DateTime? fechaAltaClienteStart = null,
        [Query] DateTime? fechaAltaClienteEnd = null);

    [Get("/api/fi-clientes/persona/{personaId}/perfil")]
    Task<ApiResponseDto<PerfilDto>> GetPerfilByPersonaIdAsync(
    [AliasAs("personaId")] int personaId,
    CancellationToken cancellationToken = default);

    /// <summary>
    /// Crea una nueva persona con valores por defecto
    /// </summary>
    [Post("/api/fi-clientes/persona/")]
    Task<ApiResponseDto<int>> CreatePersonaDefault([Body] CreatePersonaDefaultDto model);

    /// <summary>
    /// Obtiene los datos de persona física por ID
    /// </summary>
    [Get("/api/fi-clientes/persona/{id}/fisica")]
    Task<IApiResponse<ApiResponseDto<PersonaFisicaEditDto>>> GetPersonaFisicaById(int id);

    /// <summary>
    /// Guarda los datos de persona física
    /// </summary>
    [Put("/fi-clientes/persona/{id}/fisica")]
    Task<IApiResponse<ApiResponseDto>> SavePersonaFisica(int id, [Body] PersonaFisicaEditDto model);

    /// <summary>
    /// Obtiene la sección de edición del cliente por ID
    /// </summary>
    [Get("/api/fi-clientes/persona/{id}/seccion-edit")]
    Task<ApiResponseDto<ClienteEditDto>> GetSeccionClienteEdit(int id);

    /// <summary>
    /// Guarda la sección de edición del cliente
    /// </summary>
    [Put("/api/fi-clientes/persona/{id}/seccion-edit")]
    Task<ApiResponseDto> SaveSeccionClienteEdit(int id, [Body] ClienteEditDto model);

    /// <summary>
    /// Elimina una persona por ID
    /// </summary>
    [Delete("/api/fi-clientes/persona/{id}")]
    Task<IApiResponse<ApiResponseDto>> DeletePersona(int id);

    #endregion

    #region Domicilio

    /// <summary>
    /// Crea un nuevo domicilio para una persona
    /// </summary>
    [Post("/api/fi-clientes/domicilio/{personaId}")]
    Task<IApiResponse<ApiResponseDto>> CreateDomicilio(int personaId, [Body] DomicilioEditDto model);

    /// <summary>
    /// Actualiza un domicilio
    /// </summary>
    [Put("/api/fi-clientes/domicilio/{id}")]
    Task<IApiResponse<ApiResponseDto>> UpdateDomicilio(int id, [Body] DomicilioEditDto model);

    /// <summary>
    /// Elimina un domicilio por ID
    /// </summary>
    [Delete("/api/fi-clientes/domicilio/{id}")]
    Task<IApiResponse<ApiResponseDto>> DeleteDomicilio(int id);

    #endregion

    #region CuentaBancaria

    /// <summary>
    /// Obtiene cuentas bancarias filtradas y paginadas
    /// </summary>
    [Get("/api/fi-clientes/cuenta-bancaria/")]
    Task<IApiResponse<ApiResponseDto<PagedResultDto<CuentaBancariaListItemDto>>>> GetCuentasBancarias(
        [Query] int personaId = 0,
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int pageSize = 10,
        [Query] string? sortColumn = null,
        [Query] bool sortDesc = false);

    /// <summary>
    /// Obtiene una cuenta bancaria por ID
    /// </summary>
    [Get("/api/fi-clientes/cuenta-bancaria/{id}")]
    Task<IApiResponse<ApiResponseDto<CuentaBancariaEditDto>>> GetCuentaBancariaById(int id);

    /// <summary>
    /// Crea una nueva cuenta bancaria para una persona
    /// </summary>
    [Post("/api/fi-clientes/cuenta-bancaria/{personaId}")]
    Task<IApiResponse<ApiResponseDto>> CreateCuentaBancaria(int personaId, [Body] CuentaBancariaEditDto model);

    /// <summary>
    /// Actualiza una cuenta bancaria
    /// </summary>
    [Put("/api/fi-clientes/cuenta-bancaria/{id}")]
    Task<IApiResponse<ApiResponseDto>> UpdateCuentaBancaria(int id, [Body] CuentaBancariaEditDto model);

    /// <summary>
    /// Elimina una cuenta bancaria por ID
    /// </summary>
    [Delete("/api/fi-clientes/cuenta-bancaria/{id}")]
    Task<ApiResponseDto> DeleteCuentaBancaria(int id);

    #endregion

    #region Telefono

    /// <summary>
    /// Obtiene teléfonos filtrados y paginados
    /// </summary>
    [Get("/api/fi-clientes/telefono/")]
    Task<ApiResponseDto<PagedResultDto<TelefonoListItemDto>>> GetTelefonos(
        [Query] int personaId = 0,
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int pageSize = 10,
        [Query] string? sortColumn = null,
        [Query] bool sortDesc = false);

    /// <summary>
    /// Obtiene un teléfono por ID
    /// </summary>
    [Get("/api/fi-clientes/telefono/{id}")]
    Task<IApiResponse<ApiResponseDto<TelefonoEditDto>>> GetTelefonoById(int id);

    /// <summary>
    /// Actualiza un teléfono
    /// </summary>
    [Put("/api/fi-clientes/telefono/{id}")]
    Task<IApiResponse<ApiResponseDto>> UpdateTelefono(int id, [Body] TelefonoEditDto model);

    /// <summary>
    /// Elimina un teléfono por ID
    /// </summary>
    [Delete("/api/fi-clientes/telefono/{id}")]
    Task<IApiResponse<ApiResponseDto>> DeleteTelefono(int id);

    #endregion

    #region SeccionPersona

    /// <summary>
    /// Sincroniza las secciones de persona
    /// </summary>
    [Post("/api/fi-clientes/seccion-persona/sync")]
    Task<ApiResponseDto> SyncSeccionPersona([Body] List<SeccionPersonaDto> model);

    [Get("/api/fi-clientes/perfiles/activos")]
    Task<ApiResponseDto<List<PerfilDto>>> GetPerfilesActivosAsync(
        CancellationToken cancellationToken = default);

    [Get("/api/fi-clientes/secciones/by-perfil/{perfilId}")]
    Task<ApiResponseDto<List<SeccionPersonaDto>>> GetSeccionesByPerfilIdAsync(
        int perfilId,
        CancellationToken cancellationToken = default);
    #endregion
}