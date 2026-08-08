using Microsoft.AspNetCore.Components;
using Yggdrasil.Module.Layout.UI.Services.Layout;
using Yggdrasil.Module.Layout.UI.Services.Layout.DTOs;

namespace Yggdrasil.Module.Layout.UI.Components.NavMenus;

public partial class NavMenu
{
    [Inject]
    public ILayoutApi LayoutService { get; set; } = null!;

    public HashSet<AccessPointDto>? NavMenuItems { get; set; }

    protected override void OnInitialized()
    {
        _layoutState.NavIsOpenEvent += () => StateHasChanged();
        _themeState.IsDarkChangeEvent += OnThemeChanged;
    }
    private void OnThemeChanged()
    {
        InvokeAsync(StateHasChanged);
    }
    private async Task<HashSet<AccessPointDto>> InitMenu()
    {
        var menus = await LayoutService.GetNavbar();

        return menus.Data!;

    }
    private void NavTo(AccessPointDto item)
    {
        _layoutState.NavTo(item);
    }
    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            NavMenuItems = await InitMenu();
            StateHasChanged();
        }
    }

    public void Dispose()
    {
        _themeState.IsDarkChangeEvent -= OnThemeChanged;
    }
}
