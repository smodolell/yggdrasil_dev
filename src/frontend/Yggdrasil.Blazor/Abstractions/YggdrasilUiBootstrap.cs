//using Microsoft.Extensions.DependencyInjection;
//using Refit;
//using Yggdrasil.Blazor.Handlers;

//namespace Yggdrasil.Blazor.Abstractions;

//public static class YggdrasilUiBootstrap
//{
//    public static IServiceCollection AddYggdrasilModule<TModule, TApiClient>(
//        this IServiceCollection services,
//        string baseAddress)
//        where TModule : class, IUiModule
//        where TApiClient : class
//    {
//        // Registro la definición del módulo para el DiscoveryService
//        services.AddSingleton<IUiModule, TModule>();

//        // Configuro Refit para este módulo específico
//        services.AddRefitClient<TApiClient>()
//            .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseAddress))
//            .AddHttpMessageHandler<YggdrasilHeaderHandler>();

//        return services;
//    }
//}