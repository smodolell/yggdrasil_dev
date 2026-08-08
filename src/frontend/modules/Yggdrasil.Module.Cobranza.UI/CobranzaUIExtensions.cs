using Blazilla.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Yggdrasil.Blazor.Extensions;
using Yggdrasil.Module.Cobranza.UI.Services.Catalogos;

namespace Yggdrasil.Module.Cobranza.UI;

public static class CobranzaUIExtensions
{
    public static IServiceCollection AddCobranzaUIModule(this IServiceCollection services, string apiUri)
    {
        services.AddValidatorsFromAssemblyContaining<CobranzaUIModule>();

        // Usamos el extension del Kernel para registrar el Módulo y Refit
        services.AddYggdrasilModule<CobranzaUIModule, ICatalogosApi>(apiUri);

        return services;
    }
}