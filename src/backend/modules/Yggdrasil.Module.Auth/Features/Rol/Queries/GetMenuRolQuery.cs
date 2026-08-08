using Yggdrasil.Module.Auth.Features.Rol.DTOs;

namespace Yggdrasil.Module.Auth.Features.Rol.Queries;

public class GetMenuRolQuery : IQuery<Result<List<MenuTreeItemDto>>>
{
    public required int RolId { get; set; }
}

public class GetMenuRolQueryHandler(
    IApplicationDbContext context,
    IApplicationSettingService appConfig
) : IQueryHandler<GetMenuRolQuery, Result<List<MenuTreeItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ApplicationSettingDto _appConfig = appConfig.GetApplicationSetting();

    public async Task<Result<List<MenuTreeItemDto>>> HandleAsync(GetMenuRolQuery message, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _context.SYS_Menu
                .Where(r => r.SYS_AccessPoint.Any(a => a.ApplicationId == _appConfig.ApplicationId))
                .Select(s => new MenuTreeItemDto
                {
                    MenuName = s.Name,
                    MenuId = s.Id,
                    MenuIcon = s.Icon,
                })
                .OrderBy(o => o.MenuName)
                .ToListAsync(cancellationToken);

            foreach (var item in result)
            {
                item.Childs = _context.SYS_AccessPoint
                    .Include(i => i.SYS_Plugin)
                    .Where(r => r.MenuId == item.MenuId && r.SYS_Plugin.ApplicationId == _appConfig.ApplicationId)
                    .Select(s => new MenuTreeItemDto
                    {
                        MenuName = s.AccessPointName,
                        Id = s.Id,
                        IsChecked = _context.SYS_RolAccessPoint.Any(r => r.AccessPointId == s.Id && r.RolId == message.RolId),
                    })
                    .OrderBy(o => o.MenuName)
                    .ToList();

                item.IsChecked = item.Childs.All(a => a.IsChecked);
            }

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
