using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Yggdrasil.Module.Audit;

public class AuditModule : IModule
{
    public IServiceCollection Add(IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(AuditModule).Assembly);
        TypeAdapterConfig.GlobalSettings.Compile();

        services.AddValidatorsFromAssemblyContaining<AuditModule>();

        //services.AddLiteBus(bus =>
        //{

        //    var assembly = typeof(AuditModule).Assembly;
        //    bus.AddMessaging(_ => { });
        //    bus.AddCommands(commands => commands.RegisterFromAssembly(assembly));
        //    bus.AddQueries(queries => queries.RegisterFromAssembly(assembly));
        //});

        return services;
    }

    public Guid GetModuleId() => Guid.Parse("4296c06a-6cd4-4db9-a8a9-4472dcea0961");

    public string GetModuleName() => "Audit";

    public string GetModuleDescription() => "Módulo de Auditoría y Log de Accesos";
}
