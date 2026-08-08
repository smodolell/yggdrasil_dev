using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using LiteBus.Queries;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Yggdrasil.Module.Catalog;

public class CatalogModule : IModule
{
    public IServiceCollection Add(IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(CatalogModule).Assembly);
        TypeAdapterConfig.GlobalSettings.Compile();

        services.AddValidatorsFromAssemblyContaining<CatalogModule>();

     
        return services;
    }
    public string GetModuleDescription()
    {
        return "Modulos de Catalogs";
    }

    public Guid GetModuleId()
    {
        return Guid.Parse("eae8dd8c-1535-4cfc-b0b7-6108a2340048");
    }

    public string GetModuleName()
    {
        return "Catalog";
    }

}
