using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using LiteBus.Messaging;
using LiteBus.Queries;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Yggdrasil.Module.System.Features.Sync.Commands;

namespace Yggdrasil.Module.System;

public class SystemModule : IModule
{
    public IServiceCollection Add(IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(SystemModule).Assembly);
        TypeAdapterConfig.GlobalSettings.Compile();

        services.AddValidatorsFromAssemblyContaining<SystemModule>();
        //services.AddLiteBus(bus =>
        //{
        //    var assembly = typeof(SystemModule).Assembly;

        //    bus.AddMessaging(_ => { });
        //    bus.AddCommands(m =>
        //    {
        //        m.Register(typeof(SyncAccessPointCommand));
        //        });
        //    bus.AddQueries(m => m.RegisterFromAssembly(assembly));

        //});

        return services;
    }
    public string GetModuleDescription()
    {
        return "Módulo encargado de la gestión de la configuración general del sistema, incluyendo empresas, sucursales, usuarios y roles.";
    }

    public Guid GetModuleId()
    {
        return Guid.Parse("2c8eeb35-98e5-4b99-916b-f6550159bf2d");
    }

    public string GetModuleName()
    {
        return "System";
    }

}
