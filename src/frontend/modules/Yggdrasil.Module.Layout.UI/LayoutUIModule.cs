using System.Reflection;
using Yggdrasil.Blazor.Abstractions;

namespace Yggdrasil.Module.Layout.UI;

public class LayoutUIModule : IUiModule
{
    public Guid ModuleId => Guid.Parse("A1B2C3D4-E5F6-4A7B-8C9D-0E1F2A3B4C6D");
    public string Name => "Layout de Sistema";
    public string Description => "Menu sistema";
    public string Icon => "layout"; // Nombre del icono de tu librería UI (MudBlazor/Material)

    public Assembly ModuleAssembly => typeof(LayoutUIModule).Assembly;
}
