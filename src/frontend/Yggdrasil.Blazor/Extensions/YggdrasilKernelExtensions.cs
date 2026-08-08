using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Yggdrasil.Blazor.Auth;
using Yggdrasil.Blazor.Handlers;
using Yggdrasil.Blazor.Services;

namespace Yggdrasil.Blazor.Extensions;

public static class YggdrasilKernelExtensions
{
    public static IServiceCollection AddYggdrasilKernel(this IServiceCollection services, string apiBaseUrl)
    {
        services.AddAuthorizationCore();
        services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


        // 1. Registro del Handler de Seguridad (para interceptar tokens JWT)
        services.AddTransient<YggdrasilHeaderHandler>();

        // 2. Registro de la API Core de Refit (para la sincronización global)
        // Nota: IYggdrasilCoreApi debe estar definida en este proyecto o en Contracts
        services.AddRefitClient<IYggdrasilCoreApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl))
            .AddHttpMessageHandler<YggdrasilHeaderHandler>();

        // 3. Registro del servicio de sincronización que usará la reflexión
        services.AddScoped<SystemSyncService>();

        // 4. Registro de otros servicios transversales (Estado, Notificaciones, etc.)
        // services.AddScoped<IAppState, AppState>();

        return services;
    }


}