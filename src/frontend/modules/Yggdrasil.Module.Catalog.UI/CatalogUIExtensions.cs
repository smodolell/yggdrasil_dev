using Blazilla.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Yggdrasil.Blazor.Extensions;

namespace Yggdrasil.Module.Catalog.UI;

public static class CatalogUIExtensions
{
    public static IServiceCollection AddCatalogUIModule(this IServiceCollection services, string apiUri)
    {
        services.AddValidatorsFromAssemblyContaining<CatalogUIModule>();

        services.RegisterUiModule<CatalogUIModule>();

        return services;
    }
}