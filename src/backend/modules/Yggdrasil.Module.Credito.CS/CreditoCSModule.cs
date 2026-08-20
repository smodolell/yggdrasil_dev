using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Yggdrasil.Module.Credito.CS.Features.Financial.Factories;
using Yggdrasil.Module.Credito.CS.Features.Financial.Services;
using Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Amortization;
using Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Fechas;

namespace Yggdrasil.Module.Credito.CS;

public class CreditoCSModule : IModule
{
    public IServiceCollection Add(IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(CreditoCSModule).Assembly);
        TypeAdapterConfig.GlobalSettings.Compile();

        services.AddValidatorsFromAssemblyContaining<CreditoCSModule>();

        services.AddScoped<FrancesStrategy>();
        services.AddScoped<AlemanStrategy>();
        services.AddScoped<AmericanaStrategy>();
        services.AddScoped<IAmortizationStrategyFactory, AmortizationStrategyFactory>();
        services.AddScoped<IAmortizationService, AmortizationService>();
        services.AddScoped<ICalendarioLaboralService, CalendarioLaboralService>();

        //Estrategias de fechas
        services.AddScoped<IFechasStrategies, SiguienteDiaHabilStrategies>();
        services.AddScoped<IFechasStrategies, SiguienteDiaHabilValidacionMesStrategies>();
        services.AddScoped<IFechasStrategies, UltimoDiaMesStrategy>();
        services.AddScoped<IFechasStrategies, SiguienteDiaHabilBBVAStrategies>();

        //Factoria de estrategias de fechas
        services.AddScoped<IFechasStrategiesFactory, FechasStrategiesFactory>();
        return services;
    }
    public string GetModuleDescription()
    {
        return "Módulo Crèdito Simple";
    }

    public Guid GetModuleId()
    {
        return Guid.Parse("f2792a3f-1424-4e89-9f08-5fe508ad4444");
    }

    public string GetModuleName()
    {
        return "CreditoCS";
    }

}
