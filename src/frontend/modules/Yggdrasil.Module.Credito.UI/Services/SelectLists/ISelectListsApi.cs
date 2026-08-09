namespace Yggdrasil.Module.Credito.UI.Services.SelectLists;

public interface ISelectListsApi
{
    /// <summary>
    /// Obtiene lista de Bancos
    /// </summary>
    [Get("/api/cat-select-lists/bancos")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetBancosAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Monedas
    /// </summary>
    [Get("/api/fi-select-lists/monedas")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetMonedasAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);

    ///// <summary>
    ///// Obtiene lista de Tipos de Cuenta Bancaria
    ///// </summary>
    //[Get("/api/fi-select-lists/tipos-cuenta-bancaria")]
    //Task<ApiResponseDto<List<SelectListItemDto>>> GetTiposCuentaBancariaAsync(
    //    [Query] string? searchTerm = null,
    //    [Query] int? maxResults = null,
    //    CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Tipos de Domicilio
    /// </summary>
    [Get("/api/fi-select-lists/tipos-domicilio")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetTiposDomicilioAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Tipos de Persona
    /// </summary>
    [Get("/api/fi-select-lists/tipos-persona")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetTiposPersonaAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Estados Civiles
    /// </summary>
    [Get("/api/fi-select-lists/estados-civiles")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetEstadosCivilesAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Géneros
    /// </summary>
    [Get("/api/fi-select-lists/generos")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetGenerosAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Estados
    /// </summary>
    [Get("/api/fi-select-lists/estados")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetEstadosAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Municipios, opcionalmente filtrados por Estado
    /// </summary>
    [Get("/api/fi-select-lists/municipios")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetMunicipiosAsync(
        [Query] int? estadoId = null,
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Empresas
    /// </summary>
    [Get("/api/fi-select-lists/empresas")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetEmpresasAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Tipos de Movimiento activos
    /// </summary>
    [Get("/api/fi-select-lists/tipos-movimiento")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetTiposMovimientoAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Tipos de Pago
    /// </summary>
    [Get("/api/fi-select-lists/tipos-pago")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetTiposPagoAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Formas de Pago
    /// </summary>
    [Get("/api/fi-select-lists/formas-pago")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetFormasPagoAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Tipos de Calculo
    /// </summary>
    [Get("/api/fi-select-lists/tipos-calculo")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetTiposCalculoAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Periodicidades
    /// </summary>
    [Get("/api/select-lists/periodicidades")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetPeriodicidadAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene lista de Tasas Fijas
    /// </summary>
    [Get("/api/select-lists/tasas-fijas")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetTasaFijaAsync(
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Obtiene lista de Tasas Variables
    /// </summary>
    [Get("/api/select-lists/tasas-variables")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetTasaVariableAsync(
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Obtiene lista de Tasas Variables
    /// </summary>
    [Get("/api/select-lists/tasas-iva")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetTasaIvaAsync(
        CancellationToken cancellationToken = default);
}
