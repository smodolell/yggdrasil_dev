using System.Reflection;
using Yggdrasil.Blazor.Abstractions;

namespace Yggdrasil.Module.Credito.UI;

public class CreditoUIModule : IUiModule
{
    public Guid ModuleId => Guid.Parse("ac69fa3b-7dfd-4fca-ba14-b43db50da101");
    public string Name => "Credito";
    public string Description => "Gestion de Creditos";
    public string Icon => "settings"; // Nombre del icono de tu librería UI (MudBlazor/Material)

    public Assembly ModuleAssembly => typeof(CreditoUIModule).Assembly;
}
