using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Yggdrasil.Common.Interfaces;

namespace Yggdrasil.Module.Credito;

public class CreditoModule : IModule
{
    public IServiceCollection Add(IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(CreditoModule).Assembly);
        TypeAdapterConfig.GlobalSettings.Compile();

        services.AddValidatorsFromAssemblyContaining<CreditoModule>();

        //services.AddScoped<FrancesStrategy>();
        //services.AddScoped<AlemanStrategy>();
        //services.AddScoped<AmericanaStrategy>();
        //services.AddScoped<IAmortizationStrategyFactory, AmortizationStrategyFactory>();
        //services.AddScoped<IAmortizationService, AmortizationService>();



        return services;
    }
    public string GetModuleDescription()
    {
        return "Módulo CreditFlow";
    }

    public Guid GetModuleId()
    {
        return Guid.Parse("f2792a3f-1424-4e89-9f05-5fe508ad4483");
    }

    public string GetModuleName()
    {
        return "Credito";
    }

}
