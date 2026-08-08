using Blazilla.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Yggdrasil.Blazor.Extensions;
using Yggdrasil.Module.Audit.UI.Services.Audits;

namespace Yggdrasil.Module.Audit.UI;

public static class AuditUIExtensions
{
    public static IServiceCollection AddAuditUIModule(this IServiceCollection services, string apiUri)
    {
        services.AddValidatorsFromAssemblyContaining<AuditUIModule>();

        // Usamos el extension del Kernel para registrar el Módulo y Refit
        services.AddYggdrasilModule<AuditUIModule, IAuditsApi>(apiUri);

        return services;
    }
}