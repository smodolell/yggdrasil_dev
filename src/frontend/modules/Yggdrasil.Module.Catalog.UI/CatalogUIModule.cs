using System.Reflection;
using Yggdrasil.Blazor.Abstractions;

namespace Yggdrasil.Module.Catalog.UI;

public class CatalogUIModule : IUiModule
{
    public Guid ModuleId => Guid.Parse("abda17d0-4bd2-410f-bc48-9f68f1808f0f");
    public string Name => "Catalogos de Sistema";
    public string Description => "Gestión de módulos, permisos y sincronización de puntos de acceso.";
    public string Icon => "settings";

    public Assembly ModuleAssembly => typeof(CatalogUIModule).Assembly;
}
