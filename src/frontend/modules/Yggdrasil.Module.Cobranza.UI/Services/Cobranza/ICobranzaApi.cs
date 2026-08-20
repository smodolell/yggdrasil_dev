
using Yggdrasil.Module.Cobranza.UI.Services.Cobranza.DTOs;

namespace Yggdrasil.Module.Cobranza.UI.Services.Cobranza;

public interface ICobranzaApi
{
    /// <summary>
    /// Obtiene los movimientos pendientes agrupados para caja manual (filtro por personaId o creditoId)
    /// </summary>
    [Get("/api/cob-caja-manual/caja-manual/")]
    Task<IApiResponse<ApiResponseDto<CajaManualDto>>> GetCajaManual(
        [Query] int? personaId = null,
        [Query] int? creditoId = null);

    /// <summary>
    /// Obtiene los movimientos pendientes de pago de un crédito
    /// </summary>
    [Get("/api/cob-caja-manual/caja-manual/movimientos-pendientes/{creditoId}")]
    Task<IApiResponse<ApiResponseDto<List<MovimientoPendienteDto>>>> GetMovimientosPendientes(int creditoId);

    /// <summary>
    /// Registra un pago y lo aplica contra los movimientos seleccionados del crédito
    /// </summary>
    [Post("/api/cob-caja-manual/caja-manual/pago")]
    Task<IApiResponse<ApiResponseDto<PagoResultDto>>> RegistrarPago([Body] PagoDto model);

    /// <summary>
    /// Cancela uno o varios pagos aplicados, restaurando los saldos de los movimientos
    /// </summary>
    [Post("/api/cob-cancelacion-pago/cancelacion-pago/")]
    Task<IApiResponse<ApiResponseDto>> CancelarPago([Body] CancelarPagoDto model);
}
