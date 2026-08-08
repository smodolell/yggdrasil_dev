using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using LiteBus.Messaging;
using LiteBus.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Yggdrasil.Common.Interfaces;

namespace Yggdrasil.Common.Extensions;


public static class ModuleExtensions
{
    public static IServiceCollection AddModules(this IServiceCollection services, params Assembly[] assemblies)
    {
        var moduleType = typeof(IModule);

        // Si no pasas assemblies, buscamos en todos los cargados que sigan tu prefijo
        var targetAssemblies = assemblies.Any()
            ? assemblies
            : AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName!.StartsWith("Yggdrasil")); // Filtro por tu namespace

        var modules = targetAssemblies
            .SelectMany(a => a.GetExportedTypes())
            .Where(t => moduleType.IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false })
            .Select(Activator.CreateInstance)
            .Cast<IModule>();

        foreach (var module in modules)
        {
            // El módulo registra sus propios servicios (DBContext, Repositorios, etc.)
            module.Add(services);

            // Lo registramos como Singleton para poder usarlo en el pipeline de 'Use' después
            services.AddSingleton(module);
        }

        // 2. REGISTRO CENTRALIZADO DE LITEBUS
        services.AddLiteBus(bus =>
        {
            bus.AddMessaging(_ => { });

            bus.AddCommands(c =>
            {
                // Iteramos y registramos comando por comando desde cada assembly
                foreach (var assembly in targetAssemblies)
                {
                    c.RegisterFromAssembly(assembly);
                }
            });

            bus.AddQueries(q =>
            {
                // Iteramos y registramos query por query desde cada assembly
                foreach (var assembly in targetAssemblies)
                {
                    q.RegisterFromAssembly(assembly);
                }
            });

            // Si llegas a usar Eventos en el futuro:
            // bus.AddEvents(e => 
            // {
            //     foreach (var assembly in targetAssemblies) e.RegisterFromAssembly(assembly);
            // });
        });

        return services;
    }
}

