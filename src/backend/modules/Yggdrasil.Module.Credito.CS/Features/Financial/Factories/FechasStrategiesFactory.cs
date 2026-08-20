using Microsoft.Extensions.DependencyInjection;
using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Fechas;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Factories;

public class FechasStrategiesFactory : IFechasStrategiesFactory
{
    private readonly IEnumerable<IFechasStrategies> _strategies;
    public FechasStrategiesFactory(IEnumerable<IFechasStrategies> strategies)
    {
        _strategies = strategies;
    }
    public async Task<List<DateTime>> GenerarCalendarioAjustadoAsync(
        AmortizationDto amortization,
        DateGenerationContext context)
    {
        // 1. Lógica algorítmica basada en el Objeto + N Parámetros para decidir la estrategia
        IFechasStrategies strategy = ResolveStrategy(amortization, context);

        // 2. Ejecutar la estrategia seleccionada
        return await strategy.GenerarCalendarioFechasAsync(amortization);
    }

    private IFechasStrategies ResolveStrategy(AmortizationDto amortization, DateGenerationContext context)
    {


        if (context.Fondeador == "SANTANDER")
        {
            var strategyDiaHabilValidacionMes = _strategies.OfType<SiguienteDiaHabilValidacionMesStrategies>()
                .FirstOrDefault();
            if (strategyDiaHabilValidacionMes == null)
                throw new InvalidOperationException("No se encontró la estrategia SiguienteDiaHabilStrategies registrada.");
            return strategyDiaHabilValidacionMes;
        }
        else if (context.Fondeador == "SOLVIMAS")
        {
            var strategyUltimoDiaMes = _strategies.OfType<UltimoDiaMesStrategy>()
                .FirstOrDefault();
            if (strategyUltimoDiaMes == null)
                throw new InvalidOperationException("No se encontró la estrategia UltimoDiaMesStrategy registrada.");
            return strategyUltimoDiaMes;

        }
        else if (context.Fondeador == "BBVA")
        { 
             var strategySiguienteDiaHabilBBVAStrategies = _strategies.OfType<SiguienteDiaHabilBBVAStrategies>()
                 .FirstOrDefault();
            if (strategySiguienteDiaHabilBBVAStrategies == null)
                throw new InvalidOperationException("No se encontró la estrategia SiguienteDiaHabilBBVAStrategies registrada.");
            return strategySiguienteDiaHabilBBVAStrategies;
        }



        var strategyDiaHabil = _strategies.OfType<SiguienteDiaHabilStrategies>().FirstOrDefault();
        if (strategyDiaHabil == null)
            throw new InvalidOperationException("No se encontró la estrategia SiguienteDiaHabilStrategies registrada.");
        return strategyDiaHabil;
    }
}