using Microsoft.Extensions.DependencyInjection;
using Refit;
using Yggdrasil.Blazor.Abstractions;
using Yggdrasil.Blazor.Handlers;
using Yggdrasil.Blazor.Services;

namespace Yggdrasil.Blazor.Extensions;

public static class YggdrasilBootstrapExtensions
{
    public static IServiceCollection AddYggdrasilModule<TModule, TInterface>(
        this IServiceCollection services,
        string apiBaseAddress)
        where TModule : class, IUiModule
        where TInterface : class
    {
        // 1. Registro Único del Módulo
        // Validamos si ya existe un registro para IUiModule con este tipo concreto
        if (!services.Any(x => x.ServiceType == typeof(IUiModule) && x.ImplementationType == typeof(TModule)))
        {
            services.AddSingleton<IUiModule, TModule>();
        }

        // 2. Registro Único del SystemSyncService (Evitamos registrar el servicio N veces)
        if (services.All(x => x.ServiceType != typeof(SystemSyncService)))
        {
            services.AddScoped<SystemSyncService>();
        }

        // 3. Registro del Handler de Seguridad (si no existe)
        if (services.All(x => x.ServiceType != typeof(YggdrasilHeaderHandler)))
        {
            services.AddTransient<YggdrasilHeaderHandler>();
        }
        if (services.All(x => x.ServiceType != typeof(ErrorHandlerDelegatingHandler)))
        {
            services.AddTransient<ErrorHandlerDelegatingHandler>();
        }
        services.AddRefitClient<TInterface>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseAddress))
            .AddHttpMessageHandler<YggdrasilHeaderHandler>()
            .AddHttpMessageHandler<ErrorHandlerDelegatingHandler>()
            ;

        return services;
    }


}


public static class YggdrasilBootstrapExtensions2
{
    // 1. Registra la infraestructura core de los módulos (Seguridad y Sincronización)
    public static IServiceCollection RegisterYggdrasilCoreInfrastructure(this IServiceCollection services)
    {
        if (services.All(x => x.ServiceType != typeof(SystemSyncService)))
        {
            services.AddScoped<SystemSyncService>();
        }

        // Registro único de handlers si no existen
        if (services.All(x => x.ServiceType != typeof(YggdrasilHeaderHandler)))
            services.AddTransient<YggdrasilHeaderHandler>();

        if (services.All(x => x.ServiceType != typeof(ErrorHandlerDelegatingHandler)))
            services.AddTransient<ErrorHandlerDelegatingHandler>();

        return services;
    }

    // 2. Registra la infraestructura de un módulo UI concreto
    public static IServiceCollection RegisterUiModule<TModule>(this IServiceCollection services)
        where TModule : class, IUiModule
    {
        if (!services.Any(x => x.ServiceType == typeof(IUiModule) && x.ImplementationType == typeof(TModule)))
        {
            services.AddSingleton<IUiModule, TModule>();
        }

        return services;
    }
}