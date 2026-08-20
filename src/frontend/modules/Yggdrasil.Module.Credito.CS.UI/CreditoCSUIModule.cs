using System.Reflection;
using Yggdrasil.Blazor.Abstractions;

namespace Yggdrasil.Module.Credito.CS.UI;

public class CreditoCSUIModule : IUiModule
{
    public Guid ModuleId => Guid.Parse("2a9af825-4e90-4114-a629-661a6a90f999");
    public string Name => "Credito Simple";
    public string Description => "Gestion de Creditos Simples";
    public string Icon => "settings"; // Nombre del icono de tu librería UI (MudBlazor/Material)

    public Assembly ModuleAssembly => typeof(CreditoCSUIModule).Assembly;
}
