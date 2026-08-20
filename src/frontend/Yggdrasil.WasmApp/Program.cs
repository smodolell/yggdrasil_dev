using BitzArt.Blazor.Cookies;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Yggdrasil.ApiClient;

//using Yggdrasil.CreditFlow.Module.Accounting.UI;
//using Yggdrasil.CreditFlow.Module.UI;
//using Yggdrasil.CreditFlow.Module.UI.Services.Sync;
using Yggdrasil.Blazor.Extensions;
using Yggdrasil.Blazor.Handlers;
using Yggdrasil.Blazor.Services;
using Yggdrasil.Module.Audit.UI;
using Yggdrasil.Module.Catalog.UI;
using Yggdrasil.Module.Cobranza.UI;
using Yggdrasil.Module.Credito.CS.UI;
using Yggdrasil.Module.Credito.UI;
using Yggdrasil.Module.Credito.UI.Services.Sync;
using Yggdrasil.Module.Layout.UI;
using Yggdrasil.Module.Report.UI;
using Yggdrasil.Module.System.UI;
using Yggdrasil.WasmApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "";

builder.AddBlazorCookies();

builder.Services.AddYggdrasilKernel(apiBaseUrl);

builder.Services.AddLayoutUIModule(apiBaseUrl);
builder.Services.AddSystemUIModule(apiBaseUrl);
builder.Services.AddAuditUIModule(apiBaseUrl);
builder.Services.AddCatalogUIModule(apiBaseUrl);
builder.Services.AddCobranzaUIModule(apiBaseUrl);
builder.Services.AddCreditoUIModule(apiBaseUrl);
builder.Services.AddCreditoCSUIModule(apiBaseUrl);

builder.Services.AddReportesUIModule();

//builder.Services.AddDashboardUIModule(apiBaseUrl);
//builder.Services.AddCreditFlowUIModule(apiBaseUrl);
//builder.Services.AddCreditFlowAccountingUIModule(apiBaseUrl);


builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

builder.Services.ConfigureRefitClients(
    baseUrl: new Uri(apiBaseUrl),
    builder: httpClientBuilder =>
    {
        // Este bloque se ejecutará automáticamente para CADA una de las 12 APIs generadas
        httpClientBuilder
            .AddHttpMessageHandler<YggdrasilHeaderHandler>()
            .AddHttpMessageHandler<ErrorHandlerDelegatingHandler>();
    }
);

var host = builder.Build();
var syncSystemService = host.Services.GetRequiredService<SystemSyncService>();
try
{
    await syncSystemService.RunSyncAsync();
    Console.WriteLine("Yggdrasil: Módulos sincronizados con éxito.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error sincronizando: {ex.Message}");
}

var syncSeccionService = host.Services.GetRequiredService<ISeccionPersonaSyncService>();
try
{
    await syncSeccionService.SyncAllSectionsAsync();
    Console.WriteLine("Yggdrasil: Secciones Persona sincronizadas con éxito.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error sincronizando: {ex.Message}");
}
await host.RunAsync();
