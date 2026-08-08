using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Reflection;
using Yggdrasil.Blazor.Abstractions;
using Yggdrasil.Blazor.Attributes;
using Yggdrasil.Blazor.DTOs;

namespace Yggdrasil.Blazor.Services;

public class SystemSyncService(
    IEnumerable<IUiModule> modules,
    IYggdrasilCoreApi coreApi)
{
    public async Task RunSyncAsync()
    {
        var syncPayload = new List<ModuleDto>();

        foreach (var module in modules)
        {
            if (syncPayload.Any(a => a.Id == module.ModuleId)) continue;

            var pages = GetPagesFromAssembly(module.ModuleAssembly);
            syncPayload.Add(new ModuleDto
            {
                Id = module.ModuleId,
                PluginName = module.Name,
                Description = module.Description,
                Pages = pages
            });
        }

        if (syncPayload.Any())
        {
            var result = await coreApi.SyncModulesAsync(syncPayload);
            if (result.Success)
            {
                Console.WriteLine("Sincronización exitosa con el backend.");
            }
            else
            {
                Console.WriteLine($"Error en la sincronización: {result.Message}");
            }
        }
    }

    //public List<ModuleDto> DiscoverModules(params Assembly[] assemblies)
    //{
    //    var modules = new List<ModuleDto>();

    //    foreach (var assembly in assemblies)
    //    {
    //        // Buscamos la clase que implementa IModule en el ensamblado
    //        var moduleType = assembly.ExportedTypes
    //            .FirstOrDefault(t => typeof(IUiModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

    //        if (moduleType != null)
    //        {
    //            var moduleInstance = (IUiModule)Activator.CreateInstance(moduleType)!;

    //            modules.Add(new ModuleDto
    //            {
    //                Id = moduleInstance.ModuleId,
    //                PluginName = moduleInstance.Name,
    //                Description = moduleInstance.Description,
    //                Pages = GetPagesFromAssembly(assembly)
    //            });
    //        }

    //    }

    //    return modules;
    //}

    private List<PageDto> GetPagesFromAssembly(Assembly assembly)
    {
        var components = assembly.ExportedTypes
            .Where(t => t.IsSubclassOf(typeof(ComponentBase)));

        return components
           .Select(GetRouteFromComponent)
           .Where(page => page is not null)
           .Select(page => page!)
           .ToList();
    }

    private PageDto? GetRouteFromComponent(Type component)
    {
        var attributes = component.GetCustomAttributes(inherit: true);

        var routeAttr = attributes.OfType<RouteAttribute>().FirstOrDefault();
        var accessAttr = attributes.OfType<AccessPointAttribute>().FirstOrDefault();

        // Si no es una página ruteable con nuestro atributo, ignorar
        if (routeAttr is null || accessAttr is null) return null;

        var route = routeAttr.Template;

        if (string.IsNullOrEmpty(route)) return null;

        // Limpieza de parámetros de ruta {id?} para que coincida con la base de datos
        if (route.Contains('{'))
        {
            route = route.Split('{')[0].TrimEnd('/');
        }

        return new PageDto
        {
            Menu = accessAttr.Menu,
            MenuIcon = accessAttr.MenuIcon,
            MenuItem = accessAttr.ItemMenu,
            Route = route,
            AccessPointType = accessAttr.AccessPointType,
            IsAnonymous = attributes.OfType<AllowAnonymousAttribute>().Any()
        };
    }

}
