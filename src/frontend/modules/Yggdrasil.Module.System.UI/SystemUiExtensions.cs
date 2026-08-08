using Blazilla.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Yggdrasil.Blazor.Extensions;
using Yggdrasil.Module.System.UI.Services.Auth;
using Yggdrasil.Module.System.UI.Services.System;

namespace Yggdrasil.Module.System.UI;

public static class SystemUiExtensions
{
    public static IServiceCollection AddSystemUIModule(this IServiceCollection services, string apiUri)
    {
        services.AddValidatorsFromAssemblyContaining<SystemUiModule>();

        // Usamos el extension del Kernel para registrar el Módulo y Refit
        services.AddYggdrasilModule<SystemUiModule, IAuthApi>(apiUri);
        services.AddYggdrasilModule<SystemUiModule, ISystemApi>(apiUri);

        return services;
    }
}