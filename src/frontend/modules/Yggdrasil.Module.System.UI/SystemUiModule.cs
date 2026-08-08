using System.Reflection;
using Yggdrasil.Blazor.Abstractions;

namespace Yggdrasil.Module.System.UI;

public class SystemUiModule : IUiModule
{
    public Guid ModuleId => Guid.Parse("A1B2C3D4-E5F6-4A7B-8C9D-0E1F2A3B4C5D"); // ID fijo para el sistema
    public string Name => "Configuración de Sistema";
    public string Description => "Gestión de módulos, permisos y sincronización de puntos de acceso.";
    public string Icon => "settings"; // Nombre del icono de tu librería UI (MudBlazor/Material)

    public Assembly ModuleAssembly => typeof(SystemUiModule).Assembly;
}
