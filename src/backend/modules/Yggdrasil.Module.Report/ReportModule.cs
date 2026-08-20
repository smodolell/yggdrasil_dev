using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using LiteBus.Queries;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Yggdrasil.Module.Report;

public class ReportModule : IModule
{
    public IServiceCollection Add(IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(ReportModule).Assembly);
        TypeAdapterConfig.GlobalSettings.Compile();

        services.AddValidatorsFromAssemblyContaining<ReportModule>();

        
        return services;
    }

    public string GetModuleDescription() => "Módulo de Reportes con Stored Procedures";

    public Guid GetModuleId() => Guid.Parse("b1c2d3e4-f5a6-7890-abcd-ef1234567890");

    public string GetModuleName() => "Report";
}
