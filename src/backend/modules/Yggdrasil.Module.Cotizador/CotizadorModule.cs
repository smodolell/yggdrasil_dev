using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Yggdrasil.Module.System;

public class CotizadorModule : IModule
{
    public IServiceCollection Add(IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(CotizadorModule).Assembly);
        TypeAdapterConfig.GlobalSettings.Compile();

        services.AddValidatorsFromAssemblyContaining<CotizadorModule>();

        return services;
    }
    public string GetModuleDescription()
    {
        return "Módulo encargado de la Cotizacion";
    }

    public Guid GetModuleId()
    {
        return Guid.Parse("639f933c-7009-4abc-b9f7-08cfa9d1dc00");
    }

    public string GetModuleName()
    {
        return "Cotizador";
    }

}
