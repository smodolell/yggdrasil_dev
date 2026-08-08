using Blazilla.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Yggdrasil.Blazor.Extensions;
using Yggdrasil.Module.Credito.UI.Services.Clientes;
using Yggdrasil.Module.Credito.UI.Services.Configuacion;
using Yggdrasil.Module.Credito.UI.Services.SelectLists;
using Yggdrasil.Module.Credito.UI.Services.Sync;

namespace Yggdrasil.Module.Credito.UI;

public static class CreditoUIExtensions
{
    public static IServiceCollection AddCreditoUIModule(this IServiceCollection services, string apiUri)
    {
        services.AddValidatorsFromAssemblyContaining<CreditoUIModule>();

        // Usamos el extension del Kernel para registrar el Módulo y Refit
        //services.AddYggdrasilModule<CreditoUIModule, ICatalogosApi>(apiUri);
        services.AddYggdrasilModule<CreditoUIModule, IClientesApi>(apiUri);
        services.AddYggdrasilModule<CreditoUIModule, IConfiguracionApi>(apiUri);
        services.AddYggdrasilModule<CreditoUIModule, ISelectListsApi>(apiUri);
        //services.AddYggdrasilModule<CreditoUIModule, ISearchesApi>(apiUri);
        //services.AddYggdrasilModule<CreditoUIModule, ICreditosApi>(apiUri);
        //services.AddYggdrasilModule<CreditoUIModule, IProcesosApi>(apiUri);
        //services.AddYggdrasilModule<CreditoUIModule, ICobranzaApi>(apiUri);

        services.AddScoped<ISeccionPersonaSyncService, SeccionPersonaSyncService>();
        return services;
    }
}