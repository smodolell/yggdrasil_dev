using Yggdrasil.Common.DTOs;
using Yggdrasil.Common.Interfaces;

namespace Yggdrasil.Infrastructure.Services;

public class ApplicationSettingService : IApplicationSettingService
{
    private readonly ApplicationSettingDto _settings;

    public ApplicationSettingService(ApplicationSettingDto settings)
    {
        _settings = settings;
    }

    public ApplicationSettingDto GetApplicationSetting()
    {
        return _settings;
    }
}
