using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
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
using Yggdrasil.Module.System;
//using Yggdrasil.Module.Dashboard;
//using Yggdrasil.Module.Report;

var builder = WebApplication.CreateBuilder(args);
const int ApplicationId = 1;
const string ApplicationName = "Yggdrasil Financial";

var configuration = builder.Configuration;

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
    typeof(SystemModule).Assembly,
    typeof(CatalogModule).Assembly,
    typeof(AuthModule).Assembly

    //typeof(DashboardModule).Assembly,
    //typeof(CreditFlowModule).Assembly,
    //typeof(AccountingModule).Assembly
);


builder.AddWebServices();




builder.Services.AddHangfireInfrastructure(configuration);
builder.Services.AddControllers();


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
    ? new[] { "https://localhost:7025" } : new[] { "https://localhost:7025" };

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



// 4. Mapear el Dashboard de Hangfire (middleware)
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    // Opcional: configurar autorización para entornos de Producción
    // Authorization = new[] { new MiFiltroAutorizacionHangfire() }
});

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
