using Refit;
using Yggdrasil.Blazor.DTOs;
using Yggdrasil.Module.Layout.UI.Services.Layout.DTOs;

namespace Yggdrasil.Module.Layout.UI.Services.Layout;

public interface ILayoutApi
{
    [Get("/api/layout/navbar")]
    Task<ApiResponseDto<HashSet<AccessPointDto>>> GetNavbar();
}
