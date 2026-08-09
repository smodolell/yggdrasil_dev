using Refit;
using Yggdrasil.Blazor.DTOs;
using Yggdrasil.Module.Catalog.UI.Services.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.UI.Services.Catalogos;

public interface ICatalogosApi
{
    [Get("/api/cat-catalogos/banco/{id}")]
    Task<ApiResponseDto<BancoEditDto>> GetBancoById(int id, CancellationToken cancellationToken = default);

    [Get("/api/cat-catalogos/banco/")]
    Task<ApiResponseDto<PagedResultDto<BancoListItemDto>>> GetBancos(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "Id",
        [Query] bool sortDescending = false,
        CancellationToken cancellationToken = default);

    [Post("/api/cat-catalogos/banco/")]
    Task<ApiResponseDto<int>> CreateBanco([Body] BancoEditDto model, CancellationToken cancellationToken = default);

    [Put("/api/cat-catalogos/banco/{id}")]
    Task<ApiResponseDto> UpdateBanco(int id, [Body] BancoEditDto model, CancellationToken cancellationToken = default);

    [Delete("/api/cat-catalogos/banco/{id}")]
    Task<ApiResponseDto> DeleteBanco(int id, CancellationToken cancellationToken = default);


    [Get("/api/cat-catalogos/moneda/{id}")]
    Task<ApiResponseDto<MonedaEditDto>> GetMonedaById(int id, CancellationToken cancellationToken = default);

    [Get("/api/cat-catalogos/moneda/")]
    Task<ApiResponseDto<PagedResultDto<MonedaListItemDto>>> GetMonedas(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "Id",
        [Query] bool sortDescending = false,
        CancellationToken cancellationToken = default);

    [Post("/api/cat-catalogos/moneda/")]
    Task<ApiResponseDto<int>> CreateMoneda([Body] MonedaEditDto model, CancellationToken cancellationToken = default);

    [Put("/api/cat-catalogos/moneda/{id}")]
    Task<ApiResponseDto> UpdateMoneda(int id, [Body] MonedaEditDto model, CancellationToken cancellationToken = default);

    [Delete("/api/cat-catalogos/moneda/{id}")]
    Task<ApiResponseDto> DeleteMoneda(int id, CancellationToken cancellationToken = default);

    [Get("/api/cat-catalogos/periodicidad/{id}")]
    Task<ApiResponseDto<PeriodicidadEditDto>> GetPeriodicidadById(int id, CancellationToken cancellationToken = default);

    [Get("/api/cat-catalogos/periodicidad/")]
    Task<ApiResponseDto<PagedResultDto<PeriodicidadListItemDto>>> GetPeriodicidades(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "Id",
        [Query] bool sortDescending = false,
        CancellationToken cancellationToken = default);

    [Post("/api/cat-catalogos/periodicidad/")]
    Task<ApiResponseDto<int>> CreatePeriodicidad([Body] PeriodicidadEditDto model, CancellationToken cancellationToken = default);

    [Put("/api/cat-catalogos/periodicidad/{id}")]
    Task<ApiResponseDto> UpdatePeriodicidad(int id, [Body] PeriodicidadEditDto model, CancellationToken cancellationToken = default);

    [Delete("/api/cat-catalogos/periodicidad/{id}")]
    Task<ApiResponseDto> DeletePeriodicidad(int id, CancellationToken cancellationToken = default);

    [Get("/api/cat-catalogos/plazo/{id}")]
    Task<ApiResponseDto<PlazoEditDto>> GetPlazoById(int id, CancellationToken cancellationToken = default);

    [Get("/api/cat-catalogos/plazo/")]
    Task<ApiResponseDto<PagedResultDto<PlazoListItemDto>>> GetPlazos(
        [Query] int? valorPlazo,
        [Query] bool? activo,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "Id",
        [Query] bool sortDescending = false,
        CancellationToken cancellationToken = default);

    [Post("/api/cat-catalogos/plazo/")]
    Task<ApiResponseDto<int>> CreatePlazo([Body] PlazoEditDto model, CancellationToken cancellationToken = default);

    [Put("/api/cat-catalogos/plazo/{id}")]
    Task<ApiResponseDto> UpdatePlazo(int id, [Body] PlazoEditDto model, CancellationToken cancellationToken = default);

    [Delete("/api/cat-catalogos/plazo/{id}")]
    Task<ApiResponseDto> DeletePlazo(int id, CancellationToken cancellationToken = default);

    [Get("/api/cat-catalogos/tasa/{id}")]
    Task<ApiResponseDto<TasaEditDto>> GetTasaById(int id, CancellationToken cancellationToken = default);

    [Get("/api/cat-catalogos/tasa/")]
    Task<ApiResponseDto<PagedResultDto<TasaListItemDto>>> GetTasas(
        [Query] decimal? valueMin = null,
        [Query] decimal? valueMax = null,
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "Id",
        [Query] bool sortDescending = false,
        CancellationToken cancellationToken = default);

    [Post("/api/cat-catalogos/tasa/")]
    Task<ApiResponseDto<int>> CreateTasa([Body] TasaEditDto model, CancellationToken cancellationToken = default);

    [Put("/api/cat-catalogos/tasa/{id}")]
    Task<ApiResponseDto> UpdateTasa(int id, [Body] TasaEditDto model, CancellationToken cancellationToken = default);

    [Delete("/api/cat-catalogos/tasa/{id}")]
    Task<ApiResponseDto> DeleteTasa(int id, CancellationToken cancellationToken = default);

    [Patch("/api/cat-catalogos/tasa/{id}/active")]
    Task<ApiResponseDto> ChangeActiveTasa(int id, [Body] ChangeActiveTasaDto request, CancellationToken cancellationToken = default);

    [Get("/api/cat-catalogos/tasa-iva/{id}")]
    Task<ApiResponseDto<TasaIvaEditDto>> GetTasaIvaById(int id, CancellationToken cancellationToken = default);

    [Get("/api/cat-catalogos/tasa-iva/")]
    Task<ApiResponseDto<PagedResultDto<TasaIvaListItemDto>>> GetTasasIva(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "Id",
        [Query] bool sortDescending = false,
        CancellationToken cancellationToken = default);

    [Post("/api/cat-catalogos/tasa-iva/")]
    Task<ApiResponseDto<int>> CreateTasaIva([Body] TasaIvaEditDto model, CancellationToken cancellationToken = default);

    [Put("/api/cat-catalogos/tasa-iva/{id}")]
    Task<ApiResponseDto> UpdateTasaIva(int id, [Body] TasaIvaEditDto model, CancellationToken cancellationToken = default);

    [Delete("/api/cat-catalogos/tasa-iva/{id}")]
    Task<ApiResponseDto> DeleteTasaIva(int id, CancellationToken cancellationToken = default);

    [Patch("/api/cat-catalogos/tasa-iva/{id}/active")]
    Task<ApiResponseDto> ChangeActiveTasaIva(int id, [Body] ChangeActiveTasaDto request, CancellationToken cancellationToken = default);
    
    [Get("/api/cat-catalogos/tasa-variable/{id}")]
    Task<ApiResponseDto<TasaVariableDetalleDto>> GetTasaVariableById(int id, CancellationToken cancellationToken = default);

    [Get("/api/cat-catalogos/tasa-variable/")]
    Task<ApiResponseDto<PagedResultDto<TasaVariableListItemDto>>> GetTasasVariables(
        [Query] string? q = null,
        [Query] bool? activa = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "Nombre",
        [Query] bool sortDescending = false,
        CancellationToken cancellationToken = default);

    [Post("/api/cat-catalogos/tasa-variable/")]
    Task<ApiResponseDto<int>> CreateTasaVariable([Body] TasaVariableDto model, CancellationToken cancellationToken = default);

    [Put("/api/cat-catalogos/tasa-variable/{id}")]
    Task<ApiResponseDto> UpdateTasaVariable(int id, [Body] TasaVariableDto model, CancellationToken cancellationToken = default);

    [Post("/api/cat-catalogos/tasa-variable/{tasaId}/valor/")]
    Task<ApiResponseDto<int>> CreateTasaValor(int tasaId, [Body] TasaValorDto model, CancellationToken cancellationToken = default);

    [Put("/api/cat-catalogos/tasa-variable/{tasaId}/valor/{id}")]
    Task<ApiResponseDto> UpdateTasaValor(int tasaId, int id, [Body] TasaValorDto model, CancellationToken cancellationToken = default);
}