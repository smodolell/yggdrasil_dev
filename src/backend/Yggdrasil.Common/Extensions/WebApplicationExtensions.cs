using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Yggdrasil.Common.Endpoint;
using Yggdrasil.Common.Extensions;

namespace Yggdrasil.Common.Extensions;

public static class WebApplicationExtensions
{
    private static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group)
    {
        var groupName = group.GroupName ?? group.GetType().Name;

        return app
            .MapGroup($"/api/{groupName}")
            .WithTags(groupName);
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpointGroupType = typeof(EndpointGroupBase);

        // Buscamos en todos los assemblies cargados en el dominio 
        // (o puedes filtrar por nombre: a.FullName.StartsWith("Yggdrasil"))
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        var endpointGroupTypes = assemblies
            .SelectMany(a => a.GetExportedTypes())
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsSubclassOf(endpointGroupType));

        foreach (var type in endpointGroupTypes)
        {
            // Usamos Try/Catch o validamos que tenga un constructor sin parámetros
            if (Activator.CreateInstance(type) is EndpointGroupBase instance)
            {
                instance.Map(app.MapGroup(instance));
            }
        }
        //var endpointGroupType = typeof(EndpointGroupBase);

        //var assembly = Assembly.GetExecutingAssembly();

        //var endpointGroupTypes = assembly.GetExportedTypes()
        //    .Where(t => t.IsSubclassOf(endpointGroupType));

        //foreach (var type in endpointGroupTypes)
        //{
        //    if (Activator.CreateInstance(type) is EndpointGroupBase instance)
        //    {
        //        instance.Map(app.MapGroup(instance));
        //    }
        //}

        return app;
    }
}
