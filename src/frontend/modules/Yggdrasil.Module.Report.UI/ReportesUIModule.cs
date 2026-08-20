using System.Reflection;
using Yggdrasil.Blazor.Abstractions;

namespace Yggdrasil.Module.Report.UI;

public class ReportesUIModule : IUiModule
{
    public Guid ModuleId => Guid.Parse("b2007b2d-e8e8-49ef-94ab-51be5f613a89");
    public string Name => "Gestión de Reportes";
    public string Description => "Gestión de Reportes";
    public string Icon => "reports";

    public Assembly ModuleAssembly => typeof(ReportesUIModule).Assembly;
}
