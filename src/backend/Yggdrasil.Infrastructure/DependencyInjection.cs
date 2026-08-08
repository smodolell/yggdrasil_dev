using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data;
using Yggdrasil.Common.Interfaces;
using Yggdrasil.Common.Middlewares;
using Yggdrasil.Domain.Entities;
using Yggdrasil.Infrastructure.Services;
using Yggdrasil.Infrastructure.Persistence.Initializers;
using Yggdrasil.Infrastructure.Persistence;
using LiteBus.Messaging;

namespace Yggdrasil.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Configuración de Mapster
        MapsterConfig.Configure();

        services.AddMapster();


        // 2. Configuración de Base de Datos
        var connectionString = configuration.GetConnectionString("DefaultConnection");


        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Yggdrasil.Infrastructure"));
        }, ServiceLifetime.Scoped);

        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        }, ServiceLifetime.Scoped);


        services.AddIdentity<SYS_Usuario, SYS_Rol>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<QueryFactory>(sp =>
        {
            var dbContext = sp.GetRequiredService<ApplicationDbContext>();
            var connection = dbContext.Database.GetDbConnection();

            // Verificar y abrir conexión
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var compiler = new SqlServerCompiler();
            var queryFactory = new QueryFactory(connection, compiler);

            // Mantener la conexión abierta durante la vida del scope
            return queryFactory;
        });




        services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = false;

            // User settings
            options.User.RequireUniqueEmail = false;
        });

        services.AddHttpContextAccessor();

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPaginator, Paginator>();
        services.AddScoped<IDynamicSorter, DynamicSorter>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IApplicationSettingService, ApplicationSettingService>();
        services.AddScoped<IParameterExtractor, SqlServerParameterExtractor>();
        services.AddScoped<IConsecutivoService, ConsecutivoServiceV2>();
        // 2. Tu servicio de identidad
        services.AddScoped<IUserContext, UserContext>();

        services.AddHostedService<UserInitializer>();


        services.AddLiteBus(bus =>
        {
            bus.AddMessaging(_ => { });
            bus.AddCommands(module =>
            {
                module.Register(typeof(AuditoriaMiddleware<>));
            });
           

        });
        return services;
    }

}