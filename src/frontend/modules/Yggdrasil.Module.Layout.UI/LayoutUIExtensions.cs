using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using MudBlazor.Translations;
using Yggdrasil.Blazor.Extensions;
using Yggdrasil.Module.Layout.UI.Components.States;
using Yggdrasil.Module.Layout.UI.Services.Auth;
using Yggdrasil.Module.Layout.UI.Services.Layout;

namespace Yggdrasil.Module.Layout.UI;

public static class LayoutUIExtensions
{
    public static IServiceCollection AddLayoutUIModule(this IServiceCollection services, string apiUri)
    {
        services.AddMudServices();
        services.AddMudTranslations();
        services.AddScoped<ThemeState>();
        services.AddScoped<LayoutState>();

        services.AddYggdrasilModule<LayoutUIModule, ILayoutApi>(apiUri);
        services.AddYggdrasilModule<LayoutUIModule, IAuthApi>(apiUri);

        return services;
    }
}