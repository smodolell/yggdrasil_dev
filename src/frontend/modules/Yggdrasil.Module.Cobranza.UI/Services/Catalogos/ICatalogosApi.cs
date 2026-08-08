using Yggdrasil.Module.Cobranza.UI.Services.Catalogos.DTOs;

namespace Yggdrasil.Module.Cobranza.UI.Services.Catalogos;

public interface ICatalogosApi
{
    #region TipoPago

    [Get("/api/cob-catalogos/tipo-pago/")]
    Task<ApiResponseDto<List<TipoPagoListItemDto>>> GetTiposPago([Query] string? q = null);

    [Get("/api/cob-catalogos/tipo-pago/{id}")]
    Task<ApiResponseDto<TipoPagoEditDto>> GetTipoPagoById(int id);

    [Post("/api/cob-catalogos/tipo-pago/")]
    Task<ApiResponseDto<int>> CreateTipoPago([Body] TipoPagoEditDto model);

    [Put("/api/cob-catalogos/tipo-pago/{id}")]
    Task<ApiResponseDto> UpdateTipoPago(int id, [Body] TipoPagoEditDto model);

    [Delete("/api/cob-catalogos/tipo-pago/{id}")]
    Task<ApiResponseDto> DeleteTipoPago(int id);

    #endregion
}
