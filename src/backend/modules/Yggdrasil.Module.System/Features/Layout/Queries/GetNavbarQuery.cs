using Yggdrasil.Common.Constants;
using Yggdrasil.Module.System.Features.Layout.DTOs;

namespace Yggdrasil.Module.System.Features.Layout.Queries;

public class GetNavbarQuery : IQuery<Result<HashSet<AccessPointDto>>>
{
}

public class GetNavbarQueryHandler(IApplicationDbContext context, ApplicationSettingDto applicationSetting) : IQueryHandler<GetNavbarQuery, Result<HashSet<AccessPointDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ApplicationSettingDto _applicationSetting = applicationSetting;

    public async Task<Result<HashSet<AccessPointDto>>> HandleAsync(GetNavbarQuery message, CancellationToken cancellationToken = default)
    {
        var applicationId = _applicationSetting.ApplicationId;
        var menus = await _context.SYS_Menu
          .Where(r => r.SYS_AccessPoint.Any(r => r.SYS_Plugin.ApplicationId == applicationId && r.AccessPointTypeId == AppConstants.SYS_AccessPointType_LeftMenu))
          .Select(s => new AccessPointDto
          {

              MenuIcon = s.Icon,
              MenuName = s.Name,
              Childs = s.SYS_AccessPoint.Where(r => r.SYS_Plugin.ApplicationId == applicationId && r.AccessPointTypeId == AppConstants.SYS_AccessPointType_LeftMenu)
              .Select(s1 => new AccessPointDto
              {

                  MenuIcon = s1.Icon,
                  MenuName = s1.AccessPointName,
                  Route = s1.Route,
              })
              .OrderBy(o => o.MenuName)
              .ToHashSet(),

          })
          .OrderBy(o => o.MenuName)
          .ToHashSetAsync();

        return Result.Success(menus);

    }
}
