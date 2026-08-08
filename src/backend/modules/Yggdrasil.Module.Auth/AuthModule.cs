using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using LiteBus.Messaging;
using LiteBus.Queries;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Yggdrasil.Module.Auth;

public class AuthModule : IModule
{
    public IServiceCollection Add(IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(AuthModule).Assembly);
        TypeAdapterConfig.GlobalSettings.Compile();

        services.AddValidatorsFromAssemblyContaining<AuthModule>();

        //services.AddLiteBus(bus =>
        //{
        //    var assembly = typeof(AuthModule).Assembly;

        //    bus.AddMessaging(_ => { });           
        //    bus.AddCommands(m => m.RegisterFromAssembly(assembly));  
        //    bus.AddQueries(m => m.RegisterFromAssembly(assembly));   

        //});

        return services;
    }
    public string GetModuleDescription()
    {
        return "Módulo Auth.";
    }

    public Guid GetModuleId()
    {
        return Guid.Parse("f2792a3f-1424-4e89-9f05-5fe508ad4484");
    }

    public string GetModuleName()
    {
        return "Auth";
    }

}
