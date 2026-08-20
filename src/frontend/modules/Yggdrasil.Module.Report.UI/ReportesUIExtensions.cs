using Blazilla.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Yggdrasil.Blazor.Extensions;

namespace Yggdrasil.Module.Report.UI;

public static class ReportesUIExtensions
{
    public static IServiceCollection AddReportesUIModule(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ReportesUIModule>();
        
        services.RegisterUiModule<ReportesUIModule>();

        return services;
    }
}