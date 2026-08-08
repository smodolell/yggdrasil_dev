using System.Reflection;
using Yggdrasil.Blazor.Abstractions;

namespace Yggdrasil.Module.Audit.UI;

public class AuditUIModule : IUiModule
{
    public Guid ModuleId => Guid.Parse("d76a26d9-1621-4d1c-aea6-ef565e927271");
    public string Name => "Módulo de Auditoría";
    public string Description => "Gestión de auditorías, eventos y reportes .";
    public string Icon => "settings";

    public Assembly ModuleAssembly => typeof(AuditUIModule).Assembly;
}
