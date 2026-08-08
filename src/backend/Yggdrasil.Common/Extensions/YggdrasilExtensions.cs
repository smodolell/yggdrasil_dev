using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Yggdrasil.Common.DTOs;

namespace Yggdrasil.Common.Extensions;

public static class YggdrasilExtensions
{

    public static IServiceCollection AddYggdrasilApplication(this IServiceCollection services, Action<ApplicationSettingDto> configureOptions)
    {
        // Configura las opciones en el contenedor de servicios
        services.Configure(configureOptions);

        // Registra AppConfig como Singleton usando las opciones configuradas
        services.AddSingleton<ApplicationSettingDto>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<ApplicationOptions>>().Value;
            return new ApplicationSettingDto(options);
        });

        return services;
    }






}