using Refit;
using Yggdrasil.Blazor.DTOs;

namespace Yggdrasil.Blazor.Services;

public interface IYggdrasilCoreApi
{
    [Post("/api/sync-application/module")]
    Task<ApiResponseDto<int>> SyncModulesAsync([Body] List<ModuleDto> modules);
}