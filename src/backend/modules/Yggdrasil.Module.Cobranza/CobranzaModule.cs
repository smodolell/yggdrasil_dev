using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Yggdrasil.Common.Interfaces;

namespace Yggdrasil.Module.Cobranza;

public class CobranzaModule : IModule
{
    public IServiceCollection Add(IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(CobranzaModule).Assembly);
        TypeAdapterConfig.GlobalSettings.Compile();

        services.AddValidatorsFromAssemblyContaining<CobranzaModule>();

        
        return services;
    }
    public string GetModuleDescription()
    {
        return "Módulo Cobranza";
    }

    public Guid GetModuleId()
    {
        return Guid.Parse("f2792a3f-1424-4e89-9f05-5fe508ad4489");
    }

    public string GetModuleName()
    {
        return "Cobranza";
    }

}
