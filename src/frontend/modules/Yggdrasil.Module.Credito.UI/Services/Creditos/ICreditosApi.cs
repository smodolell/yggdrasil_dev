using Yggdrasil.Module.Credito.UI.Services.Creditos.DTOs;

namespace Yggdrasil.Module.Credito.UI.Services.Creditos;

public interface ICreditosApi
{
    #region Créditos

    /// <summary>
    /// Obtiene créditos filtrados y paginados
    /// </summary>
    [Get("/api/fi-creditos/credito/")]
    Task<ApiResponseDto<PagedResultDto<CreditoListItemDto>>> GetCreditos(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int pageSize = 10,
        [Query] string? sortColumn = null,
        [Query] bool sortDesc = true,
        [Query] int? productoId = null,
        [Query] int? estatusCreditoId = null,
        [Query] DateTime? fechaActivacionStart = null,
        [Query] DateTime? fechaActivacionEnd = null);

    /// <summary>
    /// Obtiene el detalle completo de un crédito (crédito, cliente y producto)
    /// </summary>
    [Get("/api/fi-creditos/credito/{id}/detail")]
    Task<IApiResponse<ApiResponseDto<CreditoDetailDto>>> GetCreditoDetail(int id);

    /// <summary>
    /// Obtiene la tabla de amortización de un crédito
    /// </summary>
    [Get("/api/fi-creditos/credito/{id}/tabla-amortiza")]
    Task<IApiResponse<ApiResponseDto<List<TablaAmortizaItemDto>>>> GetTablaAmortiza(
        int id,
        [Query] int? version = null);

    /// <summary>
    /// Obtiene los movimientos de un crédito
    /// </summary>
    [Get("/api/fi-creditos/credito/{id}/movimientos")]
    Task<IApiResponse<ApiResponseDto<List<MovimientoItemDto>>>> GetMovimientos(int id);

    /// <summary>
    /// Obtiene los pagos de un crédito (creditoId) o de todos los créditos de una persona (personaId)
    /// </summary>
    [Get("/api/fi-creditos/credito/pagos")]
    Task<IApiResponse<ApiResponseDto<List<PagoItemDto>>>> GetPagos(
        [Query] int? personaId = null,
        [Query] int? creditoId = null);

    /// <summary>
    /// Obtiene el detalle de un pago
    /// </summary>
    [Get("/api/fi-creditos/credito/pagos/{pagoId}/detail")]
    Task<IApiResponse<ApiResponseDto<PagoDetailDto>>> GetPagoDetailById(int pagoId);

    #endregion

    #region Operaciones

    /// <summary>
    /// Obtiene la plantilla de nuevo crédito para una persona
    /// </summary>
    [Get("/api/fi-operaciones/credito/new/{personaId}")]
    Task<IApiResponse<ApiResponseDto<CreditoEditDto>>> GetNewCredito(int personaId);

    /// <summary>
    /// Obtiene los datos de un crédito por ID
    /// </summary>
    [Get("/api/fi-operaciones/credito/{id}")]
    Task<IApiResponse<ApiResponseDto<CreditoEditDto>>> GetCreditoById(int id);

    /// <summary>
    /// Crea un nuevo crédito
    /// </summary>
    [Post("/api/fi-operaciones/credito/")]
    Task<IApiResponse<ApiResponseDto<int>>> CreateCredito([Body] CreditoEditDto model);

    /// <summary>
    /// Actualiza los datos de un crédito
    /// </summary>
    [Put("/api/fi-operaciones/credito/{id}")]
    Task<IApiResponse<ApiResponseDto>> UpdateCredito(int id, [Body] CreditoEditDto model);

    /// <summary>
    /// Activa un crédito mediante el procedimiento almacenado
    /// </summary>
    [Post("/api/fi-operaciones/credito/{id}/activar")]
    Task<IApiResponse<ApiResponseDto<ActivarCreditoResultDto>>> ActivarCredito(int id);

    #endregion
}
