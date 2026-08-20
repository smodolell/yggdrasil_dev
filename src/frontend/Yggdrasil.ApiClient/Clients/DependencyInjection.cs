

#nullable enable
namespace Yggdrasil.ApiClient
{
    using System;
    using System.Net.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Http.Resilience;
    using Refit;

    /// <summary>
    /// Extension methods for configuring Refit clients in the service collection.
    /// </summary>
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Configures the Refit clients for dependency injection.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        /// <param name="baseUrl">The base URL for the API clients.</param>
        /// <param name="builder">Optional action to configure the HTTP client builder.</param>
        /// <param name="settings">Optional Refit settings to customize serialization and other behaviors.</param>
        /// <returns>The configured service collection.</returns>
        public static IServiceCollection ConfigureRefitClients(
            this IServiceCollection services, 
            Uri baseUrl, 
            Action<IHttpClientBuilder>? builder = default, 
            RefitSettings? settings = default)
        {
            var clientBuilderIAuditoriaApi = services
                .AddRefitClient<IAuditoriaApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderIAuditoriaApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderIAuditoriaApi);

            var clientBuilderIAutenticaciónApi = services
                .AddRefitClient<IAutenticaciónApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderIAutenticaciónApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderIAutenticaciónApi);

            var clientBuilderICatalogoApi = services
                .AddRefitClient<ICatalogoApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICatalogoApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICatalogoApi);

            var clientBuilderICatalogoSelectListsApi = services
                .AddRefitClient<ICatalogoSelectListsApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICatalogoSelectListsApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICatalogoSelectListsApi);

            var clientBuilderICobranzaApi = services
                .AddRefitClient<ICobranzaApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICobranzaApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICobranzaApi);

            var clientBuilderICobranzaIntradiasApi = services
                .AddRefitClient<ICobranzaIntradiasApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICobranzaIntradiasApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICobranzaIntradiasApi);

            var clientBuilderICreditoClientesApi = services
                .AddRefitClient<ICreditoClientesApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICreditoClientesApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICreditoClientesApi);

            var clientBuilderICreditoConfiguracionApi = services
                .AddRefitClient<ICreditoConfiguracionApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICreditoConfiguracionApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICreditoConfiguracionApi);

            var clientBuilderICreditoCreditosApi = services
                .AddRefitClient<ICreditoCreditosApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICreditoCreditosApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICreditoCreditosApi);

            var clientBuilderICreditoOperacionesApi = services
                .AddRefitClient<ICreditoOperacionesApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICreditoOperacionesApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICreditoOperacionesApi);

            var clientBuilderICreditoSelectListsApi = services
                .AddRefitClient<ICreditoSelectListsApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICreditoSelectListsApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICreditoSelectListsApi);

            var clientBuilderICreditoCSCatalogosApi = services
                .AddRefitClient<ICreditoCSCatalogosApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICreditoCSCatalogosApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICreditoCSCatalogosApi);

            var clientBuilderICreditoCSConfiguracionApi = services
                .AddRefitClient<ICreditoCSConfiguracionApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICreditoCSConfiguracionApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICreditoCSConfiguracionApi);

            var clientBuilderICreditoCSCreditosApi = services
                .AddRefitClient<ICreditoCSCreditosApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICreditoCSCreditosApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICreditoCSCreditosApi);

            var clientBuilderICreditoCSOperacionApi = services
                .AddRefitClient<ICreditoCSOperacionApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICreditoCSOperacionApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICreditoCSOperacionApi);

            var clientBuilderICreditoCSSelectListsApi = services
                .AddRefitClient<ICreditoCSSelectListsApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderICreditoCSSelectListsApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderICreditoCSSelectListsApi);

            var clientBuilderIReportesApi = services
                .AddRefitClient<IReportesApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderIReportesApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderIReportesApi);

            var clientBuilderISistemaApi = services
                .AddRefitClient<ISistemaApi>(settings)
                
                .ConfigureHttpClient(c => c.BaseAddress = baseUrl);

            clientBuilderISistemaApi
                .AddStandardResilienceHandler(config =>
                {
                    config.Retry = new HttpRetryStrategyOptions
                    {
                        UseJitter = true,
                        MaxRetryAttempts = 6,
                        Delay = TimeSpan.FromSeconds(1)
                    };
                });

            builder?.Invoke(clientBuilderISistemaApi);

            return services;
        }
    }
}

