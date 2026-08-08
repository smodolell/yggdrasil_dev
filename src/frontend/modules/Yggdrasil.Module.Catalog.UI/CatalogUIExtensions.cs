using Blazilla.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Yggdrasil.Blazor.Extensions;
using Yggdrasil.Module.Catalog.UI.Services.Catalogos;
using Yggdrasil.Module.Catalog.UI.Services.SelectLists;

namespace Yggdrasil.Module.Catalog.UI;

public static class CatalogUIExtensions
{
    public static IServiceCollection AddCatalogUIModule(this IServiceCollection services, string apiUri)
    {
        services.AddValidatorsFromAssemblyContaining<CatalogUIModule>();

        // Usamos el extension del Kernel para registrar el Módulo y Refit
        services.AddYggdrasilModule<CatalogUIModule, ICatalogosApi>(apiUri);
        services.AddYggdrasilModule<CatalogUIModule, ISelectListsApi>(apiUri);

        return services;
    }
}