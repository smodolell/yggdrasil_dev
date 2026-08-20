using Yggdrasil.Module.Cobranza.UI.Services.Creditos.DTOs;

namespace Yggdrasil.Module.Cobranza.UI.Services.Creditos;

public interface ICreditosApi
{
    /// <summary>
    /// Busca créditos por clave o nombre de cliente (usado por el selector de crédito)
    /// </summary>
    [Get("/api/fi-creditos/credito/")]
    Task<ApiResponseDto<PagedResultDto<CreditoBusquedaDto>>> GetCreditos(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int pageSize = 10,
        [Query] int? estatusCreditoId = null,
        CancellationToken cancellationToken = default);
}
