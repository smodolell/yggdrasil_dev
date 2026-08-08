using Microsoft.AspNetCore.Components;

namespace Yggdrasil.Module.Layout.UI.Components;

public partial class AuthorizedLayout
{
    [Parameter] public RenderFragment? Child { get; set; }
}
