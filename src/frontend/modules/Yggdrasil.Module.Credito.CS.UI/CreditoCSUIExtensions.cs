using Blazilla.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Yggdrasil.Blazor.Extensions;

namespace Yggdrasil.Module.Credito.CS.UI;

public static class CreditoCSUIExtensions
{
    public static IServiceCollection AddCreditoCSUIModule(this IServiceCollection services, string apiUri)
    {
        services.AddValidatorsFromAssemblyContaining<CreditoCSUIModule>();

        // Los clientes Refit (ICreditoCSConfiguracionApi, ICreditoCSCreditosApi) ya se registran
        // de forma centralizada en Yggdrasil.ApiClient.ConfigureRefitClients, por lo que aquí
        // solo registramos el módulo en el sistema de descubrimiento de UI.
        services.RegisterUiModule<CreditoCSUIModule>();

        return services;
    }
}
