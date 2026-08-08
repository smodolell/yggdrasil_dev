using Refit;
using Yggdrasil.Blazor.DTOs;
using Yggdrasil.Module.System.UI.Services.System.DTOs;

namespace Yggdrasil.Module.System.UI.Services.System;

public interface ISystemApi
{
    [Post("/api/sync-application/module")]
    Task<IApiResponse<int>> SyncModuleAsync([Body] List<ModuleDto> modules);

    #region Empresa


    [Get("/api/configuracion/empresa/{id}")]
    Task<ApiResponseDto<EmpresaDto>> GetEmpresaById(
        [AliasAs("id")] int id,
        CancellationToken cancellationToken = default);


    [Get("/api/configuracion/empresa/")]
    Task<ApiResponseDto<PagedResultDto<EmpresaListItemDto>>> GetPaginatedEmpresa(
        [Query] string? q = null,
        [Query] int page = 1,
        [Query] int size = 10,
        [Query] string sortColumn = "Id",
        [Query] bool sortDescending = false,
        CancellationToken cancellationToken = default);

    [Post("/api/configuracion/empresa/")]
    Task<ApiResponseDto<int>> CreateEmpresa(
        [Body] EmpresaEditDto model,
        CancellationToken cancellationToken = default);

    [Put("/api/configuracion/empresa/{id}")]
    Task<ApiResponseDto> UpdateEmpresa(
        [AliasAs("id")] int id,
        [Body] EmpresaEditDto model,
        CancellationToken cancellationToken = default);

    #endregion
}
