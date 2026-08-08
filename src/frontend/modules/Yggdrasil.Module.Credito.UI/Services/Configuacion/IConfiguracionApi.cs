using Yggdrasil.Module.Credito.UI.Services.Configuacion.DTOs;

namespace Yggdrasil.Module.Credito.UI.Services.Configuacion;

public interface IConfiguracionApi
{
    #region TipoMovimiento

    [Get("/api/fi-configuracion/tipo-movimiento/{id}/edit")]
    Task<ApiResponseDto<TipoMovimientoEditDto>> GetTipoMovimientoById(int id);

    /// <summary>
    /// Obtiene lista paginada de tipos de movimiento
    /// </summary>
    [Get("/api/fi-configuracion/tipo-movimiento/list")]
    Task<ApiResponseDto<PagedResultDto<TipoMovimientoListItemDto>>> GetTipoMovimientos(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int pageSize = 10,
        [Query] string? sortColumn = null,
        [Query] bool sortDesc = false,
        [Query] bool? activo = null);

    /// <summary>
    /// Activa o desactiva un tipo de movimiento
    /// </summary>
    [Patch("/api/fi-configuracion/tipo-movimiento/{id}/activo")]
    Task<ApiResponseDto> ChangeActivoTipoMovimiento(
        int id,
        [Body] ChangeActivoDto request);

    /// <summary>
    /// Crea un nuevo tipo de movimiento
    /// </summary>
    [Post("/api/fi-configuracion/tipo-movimiento/")]
    Task<ApiResponseDto<int>> CreateTipoMovimiento([Body] TipoMovimientoEditDto model);

    /// <summary>
    /// Actualiza un tipo de movimiento
    /// </summary>
    [Put("/api/fi-configuracion/tipo-movimiento/{id}")]
    Task<ApiResponseDto> UpdateTipoMovimiento(int id, [Body] TipoMovimientoEditDto model);

    /// <summary>
    /// Elimina un tipo de movimiento
    /// </summary>
    [Delete("/api/fi-configuracion/tipo-movimiento/{id}")]
    Task<ApiResponseDto> DeleteTipoMovimiento(int id);

    #endregion

    #region Producto

    /// <summary>
    /// Obtiene productos filtrados
    /// </summary>
    [Get("/api/fi-configuracion/producto/")]
    Task<ApiResponseDto<PagedResultDto<ProductoListItemDto>>> GetProductos(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = nameof(ProductoListItemDto.Id),
        [Query] bool sortDescending = false);

    /// <summary>
    /// Obtiene un producto por ID para edición
    /// </summary>
    [Get("/api/fi-configuracion/producto/{id}")]
    Task<ApiResponseDto<ProductoEditDto>> GetProductoById(int id);

    /// <summary>
    /// Obtiene el detalle de un producto por ID
    /// </summary>
    [Get("/api/fi-configuracion/producto/{id}/detail")]
    Task<ApiResponseDto<ProductoDetailDto>> GetProductoDetail(int id);

    /// <summary>
    /// Crea un nuevo producto
    /// </summary>
    [Post("/api/fi-configuracion/producto/")]
    Task<ApiResponseDto<int>> CreateProducto([Body] ProductoCreateDto model);

    /// <summary>
    /// Actualiza un producto
    /// </summary>
    [Put("/api/fi-configuracion/producto/{id}")]
    Task<ApiResponseDto> UpdateProducto(int id, [Body] ProductoEditDto model);

    #endregion

    #region CargoInicial

    /// <summary>
    /// Obtiene los cargos iniciales de un producto
    /// </summary>
    [Get("/api/fi-configuracion/producto/{productoId}/cargo-inicial/")]
    Task<ApiResponseDto<List<CargoInicialListItemDto>>> GetCargosIniciales(int productoId);

    /// <summary>
    /// Obtiene un cargo inicial por ID para edición
    /// </summary>
    [Get("/api/fi-configuracion/cargo-inicial/{id}")]
    Task<ApiResponseDto<CargoInicialEditDto>> GetCargoInicialById(int id);

    /// <summary>
    /// Crea o actualiza un cargo inicial
    /// </summary>
    [Post("/api/fi-configuracion/cargo-inicial/")]
    Task<ApiResponseDto<int>> SaveCargoInicial([Body] CargoInicialEditDto model);

    /// <summary>
    /// Elimina un cargo inicial
    /// </summary>
    [Delete("/api/fi-configuracion/cargo-inicial/{id}")]
    Task<ApiResponseDto> DeleteCargoInicial(int id);

    #endregion

    #region ConceptoFinanciado

    /// <summary>
    /// Obtiene los conceptos financiados de un producto
    /// </summary>
    [Get("/api/fi-configuracion/producto/{productoId}/concepto-financiado/")]
    Task<ApiResponseDto<List<ConceptoFinanciadoListItemDto>>> GetConceptosFinanciados(int productoId);

    /// <summary>
    /// Obtiene un concepto financiado por ID para edición
    /// </summary>
    [Get("/api/fi-configuracion/concepto-financiado/{id}")]
    Task<ApiResponseDto<ConceptoFinanciadoEditDto>> GetConceptoFinanciadoById(int id);

    /// <summary>
    /// Crea o actualiza un concepto financiado
    /// </summary>
    [Post("/api/fi-configuracion/concepto-financiado/")]
    Task<ApiResponseDto<int>> SaveConceptoFinanciado([Body] ConceptoFinanciadoEditDto model);

    /// <summary>
    /// Elimina un concepto financiado
    /// </summary>
    [Delete("/api/fi-configuracion/concepto-financiado/{id}")]
    Task<ApiResponseDto> DeleteConceptoFinanciado(int id);

    #endregion

    #region Perfil

    /// <summary>
    /// Obtiene perfiles filtrados y paginados
    /// </summary>
    [Get("/api/fi-configuracion/perfil/")]
    Task<ApiResponseDto<PagedResultDto<PerfilListItemDto>>> GetPerfiles(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int pageSize = 10,
        [Query] string? sortColumn = null,
        [Query] bool sortDesc = false,
        [Query] bool? activo = null);

    /// <summary>
    /// Obtiene un perfil por ID para edición (usar 0 para nuevo)
    /// </summary>
    [Get("/api/fi-configuracion/perfil/{id}")]
    Task<ApiResponseDto<PerfilEditDto>> GetPerfilById(int id);

    /// <summary>
    /// Crea o actualiza un perfil con sus secciones
    /// </summary>
    [Post("/api/fi-configuracion/perfil/")]
    Task<ApiResponseDto<int>> SavePerfil([Body] PerfilEditDto model);

    /// <summary>
    /// Elimina un perfil por ID
    /// </summary>
    [Delete("/api/fi-configuracion/perfil/{id}")]
    Task<ApiResponseDto> DeletePerfil(int id);

    #endregion

    #region Seccion

    /// <summary>
    /// Obtiene secciones filtradas y paginadas
    /// </summary>
    [Get("/api/fi-configuracion/seccion/")]
    Task<ApiResponseDto<PagedResultDto<SeccionListItemDto>>> GetSecciones(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int pageSize = 10,
        [Query] string? sortColumn = null,
        [Query] bool sortDesc = false);

    #endregion

    #region CalendarioLaboral

    /// <summary>
    /// Obtiene el calendario laboral paginado y filtrado
    /// </summary>
    [Get("/api/fi-configuracion/calendario-laboral/")]
    Task<ApiResponseDto<PagedResultDto<CalendarioLaboralListItemDto>>> GetPaginatedCalendarioLaboral(
        [Query] int? anio = null,
        [Query] int? mes = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = nameof(CalendarioLaboralListItemDto.Fecha),
        [Query] bool sortDescending = false);

    /// <summary>
    /// Actualiza un día del calendario laboral
    /// </summary>
    [Put("/api/fi-configuracion/calendario-laboral/{id}")]
    Task<ApiResponseDto> UpdateCalendarioLaboral(int id, [Body] CalendarioLaboralEditDto model);

    /// <summary>
    /// Genera el calendario laboral de un año
    /// </summary>
    [Post("/api/fi-configuracion/calendario-laboral/generar")]
    Task<ApiResponseDto> CreateCalendarioLaboral([Query] int? anio = null);

    /// <summary>
    /// Descarga el layout de días inhábiles
    /// </summary>
    [Get("/api/fi-configuracion/calendario-laboral/layout")]
    Task<HttpResponseMessage> GetLayoutCalendario();

    /// <summary>
    /// Importa días inhábiles desde un archivo Excel
    /// </summary>
    [Multipart]
    [Post("/api/fi-configuracion/calendario-laboral/importar-dias-inhabiles")]
    Task<ApiResponseDto> ImportarDiasInhabiles([AliasAs("archivo")] StreamPart archivo);

    #endregion
}