using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using System.Collections;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Yggdrasil.ApiService;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Common.Handlers;
using Yggdrasil.Common.Interfaces;
using Yggdrasil.Infrastructure;
using Yggdrasil.Infrastructure.Extensions;
using Yggdrasil.Module.Audit;
using Yggdrasil.Module.Auth;
using Yggdrasil.Module.Catalog;
using Yggdrasil.Module.Cobranza;
using Yggdrasil.Module.Credito;
using Yggdrasil.Module.Credito.CS;
using Yggdrasil.Module.Report;
using Yggdrasil.Module.System;
using static System.Net.WebRequestMethods;

Log.Logger = new LoggerConfiguration()
     .WriteTo.Console()                          // Logs en consola
     .WriteTo.File("logs/yggdasil-.log",           // Logs en archivo
                   rollingInterval: RollingInterval.Day,  // Un archivo por día
                   outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
     .MinimumLevel.Debug()                       // Nivel mínimo: Debug (puedes cambiarlo)
     .CreateLogger();

try
{
    Log.Information("🚀 Aplicación iniciando...");
    var builder = WebApplication.CreateBuilder(args);
    const int ApplicationId = 1;
    const string ApplicationName = "Yggdrasil Financial";

    var configuration = builder.Configuration;

    builder.Host.UseSerilog();

    builder.Services.AddYggdrasilApplication(options =>
    {
        options.ApplicationId = ApplicationId;
        options.ApplicationName = ApplicationName;
    });

    builder.Services.AddInfrastructure(configuration);

    // Add services to the container.
    builder.Services.AddModules(
        typeof(AuditModule).Assembly,
        typeof(CobranzaModule).Assembly,
        typeof(CreditoModule).Assembly,
        typeof(CreditoCSModule).Assembly,
        typeof(SystemModule).Assembly,
        typeof(CatalogModule).Assembly,
        typeof(ReportModule).Assembly,
        typeof(AuthModule).Assembly

    //typeof(DashboardModule).Assembly,
    //typeof(CreditFlowModule).Assembly,
    //typeof(AccountingModule).Assembly
    );


    builder.AddWebServices();




builder.Services.AddControllers();
        
    //    .AddJsonOptions(options =>
    //{
    //    // Ignorar referencias circulares
    //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

    //    // Ignorar propiedades nulas
    //    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

    //    // Usar camelCase
    //    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

    //    // Ignorar métodos (solo serializa propiedades)
    //    options.JsonSerializerOptions.IncludeFields = false;
    //});


    builder.Services.AddExceptionHandler<CustomExceptionHandler>();
    builder.Services.AddProblemDetails(options =>
    {
        options.CustomizeProblemDetails = context =>
        {
            context.ProblemDetails.Extensions["server"] = Environment.MachineName;
        };
    });

    builder.Services.ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    var secretKey = jwtSettings["SecretKey"];
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? ""))
        };
    });

    builder.Services.AddAuthorization();

    // Configuración de CORS
    builder.Services.AddCors(options =>
    {
        var allowedOrigins = builder.Environment.IsDevelopment()
        ? new[] { "http://localhost:5160", "https://localhost:7207" } : new[] { "http://localhost:7025", "https://localhost:7207/"};

        options.AddPolicy("YggdrasilCorsPolicy",
            policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
    });








    var app = builder.Build();



    app.UseExceptionHandler();
    app.UseStatusCodePages();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        //app.MapOpenApi();

        //app.MapScalarApiReference(options =>
        //{
        //    options.WithTitle("Yggdrasil API Documentation");
        //    options.WithTheme(ScalarTheme.Saturn);
        //    options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        //    options.HideSearch = true;// Habilita/Deshabilita el buscador (Ctrl+K)
        //    options.ShowSidebar = true; // Muestra u oculta la barra lateral
        //    options.DarkMode = true;
        //});
    }

#if (!UseAspire)
    app.UseHealthChecks("/health");
#endif
    app.UseExceptionHandler(options => { });

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    app.UseCors("YggdrasilCorsPolicy");
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Pasivos V1");
    });
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithTitle("API Services Catalogos");
        options.WithTheme(ScalarTheme.DeepSpace);
        options.WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.AsyncHttp);
        options.HideSearch = true;// Habilita/Deshabilita el buscador (Ctrl+K)
        options.ShowSidebar = true; // Muestra u oculta la barra lateral
        options.DarkMode = false;
    });


    app.Map("/", () => Results.Redirect("/scalar"));



    //app.UseHangfireDashboard("/hangfire", new DashboardOptions
    //{
    //    // Opcional: configurar autorización para entornos de Producción
    //    // Authorization = new[] { new MiFiltroAutorizacionHangfire() }
    //});

#if (UseAspire)
app.MapDefaultEndpoints();
#endif
    using (var scope = app.Services.CreateScope())
    {
        var modules = scope.ServiceProvider.GetServices<IModule>();
        foreach (var module in modules)
        {
            module.Use(app);
        }
    }
    app.MapEndpoints();

    app.MapControllers();



    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "❌ La aplicación falló inesperadamente");
    Log.Fatal("Message: {Message}", ex.Message);
    Log.Fatal("Source: {Source}", ex.Source);
    Log.Fatal("TargetSite: {TargetSite}", ex.TargetSite);
    Log.Fatal("StackTrace: {StackTrace}", ex.StackTrace);
    if (ex.InnerException != null)
    {
        Log.Fatal("InnerException: {Inner}", ex.InnerException);
        Log.Fatal("InnerMessage: {InnerMessage}", ex.InnerException.Message);
        Log.Fatal("InnerStackTrace: {InnerStackTrace}", ex.InnerException.StackTrace);
    }
    foreach (DictionaryEntry entry in ex.Data)
    {
        Log.Fatal("Data[{Key}] = {Value}", entry.Key, entry.Value);
    }

}
finally
{
    Log.CloseAndFlush();
}