using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Yggdrasil.Module.Credito.Features.Financial.Attibutes;
using Yggdrasil.Module.Credito.Features.Financial.Strategies;

namespace Yggdrasil.Module.Credito.Features.Financial.Factories;

public class AmortizationStrategyFactory : IAmortizationStrategyFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AmortizationStrategyFactory> _logger;
    private readonly Dictionary<AmortizationMethod, Type> _availableStrategies;
    public AmortizationStrategyFactory(IServiceProvider serviceProvider, ILogger<AmortizationStrategyFactory> logger)
    {
        _serviceProvider = serviceProvider;
        this._logger = logger;
        _availableStrategies = BuildStrategyDictionary();
    }

    private  Dictionary<AmortizationMethod, Type> BuildStrategyDictionary()
    {
        var strategies = new Dictionary<AmortizationMethod, Type>();

        // Obtener todas las implementaciones registradas
        var strategyTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IAmortizationStrategy).IsAssignableFrom(t)
                        && !t.IsInterface
                        && !t.IsAbstract
                        && _serviceProvider.GetService(t) != null);

        foreach (var type in strategyTypes)
        {
            var attribute = type.GetCustomAttribute<AmortizationMethodAttribute>();
            if (attribute != null && !strategies.ContainsKey(attribute.Method))
            {
                strategies[attribute.Method] = type;
            }
        }

        return strategies;
    }
    public IAmortizationStrategy GetStrategy(AmortizationMethod method)
    {
        if (!_availableStrategies.TryGetValue(method, out var strategyType))
        {
            var available = string.Join(", ", _availableStrategies.Keys);
            throw new NotSupportedException(
                $"Método {method} no está implementado o registrado. " +
                $"Métodos disponibles: {available}");
        }

        var strategy = _serviceProvider.GetRequiredService(strategyType);
        _logger.LogInformation("Usando estrategia {Strategy} para método {Method}",
            strategyType.Name, method);

        return (IAmortizationStrategy)strategy;
    }

    public List<AmortizationMethod> GetAvailableMethods()
     => _availableStrategies.Keys.ToList();
}