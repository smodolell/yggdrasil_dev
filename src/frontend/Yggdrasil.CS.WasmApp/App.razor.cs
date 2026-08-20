using System.Reflection;

namespace Yggdrasil.CS.WasmApp;

public partial class App
{
    private static List<Assembly> AdditionalAssemblies = new List<Assembly>()
    {
        
        typeof(Yggdrasil.Module.Layout.UI.Components._Imports).Assembly,
        typeof(Yggdrasil.Module.System.UI.Components._Imports).Assembly,
        typeof(Yggdrasil.Module.Catalog.UI.Components._Imports).Assembly,
        typeof(Yggdrasil.Module.Credito.CS.UI.Pages._Imports).Assembly,
        //typeof(Yggdrasil.Module.Dashboard.UI.Pages._Imports).Assembly,
        //typeof(Yggdrasil.CreditFlow.Module.UI.Components._Imports).Assembly,
        //typeof(Yggdrasil.CreditFlow.Module.Accounting.UI.Components._Imports).Assembly,
    };
}
