using Yggdrasil.Common.Constants;

namespace Yggdrasil.Module.System.Features.Sync.Commands;

public record SyncAccessPointCommand(List<ModuleDto> Modules) : ICommand<Result>;

public sealed class SyncAccessPointCommandHandler : ICommandHandler<SyncAccessPointCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly ApplicationSettingDto _applicationSetting;

    public SyncAccessPointCommandHandler(IApplicationDbContext context, ApplicationSettingDto applicationSetting)
    {
        _context = context;
        _applicationSetting = applicationSetting;

    }

    public async Task<Result> HandleAsync(SyncAccessPointCommand message, CancellationToken cancellationToken = default)
    {
        // 1. Asegurar existencia de la Aplicación
        var oApplication = await _context.SYS_Application.SingleOrDefaultAsync(r => r.Id == _applicationSetting.ApplicationId);
        if (oApplication == null)
        {
            oApplication = new SYS_Application { Id = _applicationSetting.ApplicationId };
            _context.SYS_Application.Add(oApplication);
        }
        oApplication.ApplicationName = _applicationSetting.ApplicationName;
        await _context.SaveChangesAsync();

        // 2. Obtener el rol Webmaster para asignaciones automáticas
        var oRolWebmaster = await _context.Roles.SingleOrDefaultAsync(r => r.Name != null && r.Name.ToUpper() == "WEBMASTER");

        // Mantendremos una lista de rutas procesadas para saber cuáles borrar al final
        var processedRoutes = new List<string>();
        var processedPluginIds = message.Modules.Select(m => m.Id).ToList();

        // 3. Procesar Módulos y Páginas recibidos
        foreach (var item in message.Modules)
        {
            var oPlugin = await _context.SYS_Plugin.FirstOrDefaultAsync(r => r.Id == item.Id && r.ApplicationId == oApplication.Id);
            if (oPlugin == null)
            {
                oPlugin = new SYS_Plugin { Id = item.Id, ApplicationId = oApplication.Id };
                await _context.SYS_Plugin.AddAsync(oPlugin);
            }

            oPlugin.PluginName = item.PluginName;
            oPlugin.PluginDescription = item.Description;
            oPlugin.Active = true;
            await _context.SaveChangesAsync();

            foreach (var page in item.Pages)
            {
                // Sincronizar Menú
                var oRootMenu = await _context.SYS_Menu.FirstOrDefaultAsync(r => r.Name == page.Menu);
                if (oRootMenu == null)
                {
                    oRootMenu = new SYS_Menu { Name = page.Menu };
                    _context.SYS_Menu.Add(oRootMenu);
                    await _context.SaveChangesAsync();
                }
                oRootMenu.Icon = page.MenuIcon;

                // Sincronizar Punto de Acceso (AccessPoint)
                var oAccessPoint = await _context.SYS_AccessPoint.FirstOrDefaultAsync(r =>
                    r.PluginId == oPlugin.Id &&
                    r.ApplicationId == oApplication.Id &&
                    r.Route == page.Route);

                if (oAccessPoint == null)
                {
                    oAccessPoint = new SYS_AccessPoint
                    {
                        Id = Guid.NewGuid(),
                        PluginId = oPlugin.Id,
                        ApplicationId = oApplication.Id,
                        MenuId = oRootMenu.Id,
                        Route = page.Route,
                        Order = 1
                    };
                    await _context.SYS_AccessPoint.AddAsync(oAccessPoint);
                }

                oAccessPoint.MenuId = oRootMenu.Id;
                oAccessPoint.AccessPointName = page.MenuItem;
                oAccessPoint.IsAnonymous = page.IsAnonymous;

                // Asignar tipo según el Enum (Usando tus constantes)
                oAccessPoint.AccessPointTypeId = page.AccessPointType switch
                {
                    AccessPointType.LeftMenu => AppConstants.SYS_AccessPointType_LeftMenu,
                    AccessPointType.Page => AppConstants.SYS_AccessPointType_Page,
                    AccessPointType.Element => AppConstants.SYS_AccessPointType_Element,
                    _ => oAccessPoint.AccessPointTypeId
                };

                await _context.SaveChangesAsync();
                processedRoutes.Add(oAccessPoint.Route);

                // 4. Asignar al Webmaster si es nuevo
                if (oRolWebmaster != null)
                {
                    var hasAccess = await _context.SYS_RolAccessPoint.AnyAsync(r => r.AccessPointId == oAccessPoint.Id && r.RolId == oRolWebmaster.Id);
                    if (!hasAccess)
                    {
                        _context.SYS_RolAccessPoint.Add(new SYS_RolAccessPoint
                        {
                            AccessPointId = oAccessPoint.Id,
                            RolId = oRolWebmaster.Id
                        });
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        // 5. LIMPIEZA: Desactivar Plugins que no vinieron en el comando
        var pluginsToDeactivate = await _context.SYS_Plugin
            .Where(p => p.ApplicationId == oApplication.Id && !processedPluginIds.Contains(p.Id))
            .ToListAsync();

        foreach (var p in pluginsToDeactivate) p.Active = false;

        // 6. LIMPIEZA: Borrar Rutas que ya no existen en el código
        var accessPointsToDelete = await _context.SYS_AccessPoint
            .Include(i => i.SYS_RolAccessPoint)
            .Where(r => r.ApplicationId == oApplication.Id && !processedRoutes.Contains(r.Route))
            .ToListAsync();

        foreach (var ap in accessPointsToDelete)
        {
            _context.SYS_RolAccessPoint.RemoveRange(ap.SYS_RolAccessPoint);
            _context.SYS_AccessPoint.Remove(ap);
        }

        await _context.SaveChangesAsync();

        // 7. LIMPIEZA: Menús huérfanos
        var orphanMenus = await _context.SYS_Menu
            .Where(m => !_context.SYS_AccessPoint.Any(ap => ap.MenuId == m.Id))
            .ToListAsync();

        _context.SYS_Menu.RemoveRange(orphanMenus);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}

public class ModuleDto
{
    public Guid Id { get; set; }
    public string PluginName { get; set; } = "";
    public string Description { get; set; } = "";
    public List<PageDto> Pages { get; set; } = new List<PageDto>();



}
public class PageDto
{
    public string Menu { get; set; } = "";
    public string MenuIcon { get; set; } = "";
    public string MenuItem { get; set; } = "";
    public string Route { get; set; } = "";
    public bool IsAnonymous { get; set; }
    public AccessPointType AccessPointType { get; set; }
}
public enum AccessPointType
{
    LeftMenu,
    Page,
    Element
}