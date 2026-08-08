using Refit;
using Yggdrasil.Blazor.DTOs;

namespace Yggdrasil.Module.Catalog.UI.Services.SelectLists;

public interface ISelectListsApi
{


    [Get("/api/select-lists/monedas")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetMonedaSelectListAsync(
      [Query] string? searchTerm = null,
      [Query] int? maxResults = null,
      CancellationToken cancellationToken = default);

 
    [Get("/api/select-lists/periodicidades")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetPeriodicidadSelectListAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);

    [Get("/api/select-lists/tasas-iva")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetTasaIvaSelectListAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);

    [Get("/api/select-lists/tasas-variables")]
    Task<ApiResponseDto<List<SelectListItemDto>>> GetTasaVariableSelectListAsync(
        [Query] string? searchTerm = null,
        [Query] int? maxResults = null,
        CancellationToken cancellationToken = default);
}

