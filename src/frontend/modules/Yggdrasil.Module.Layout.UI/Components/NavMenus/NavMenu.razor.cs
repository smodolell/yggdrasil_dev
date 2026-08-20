using Microsoft.AspNetCore.Components;
using Yggdrasil.Blazor.Extensions;
using Yggdrasil.Module.Layout.UI.Services.Layout;
using Yggdrasil.Module.Layout.UI.Services.Layout.DTOs;

namespace Yggdrasil.Module.Layout.UI.Components.NavMenus;

public partial class NavMenu
{
    [Inject]
    public ILayoutApi LayoutService { get; set; } = null!;

    public HashSet<AccessPointDto>? NavMenuItems { get; set; }

    private string _userName = string.Empty;
    private string _fullName = string.Empty;

    protected override void OnInitialized()
    {
        _layoutState.NavIsOpenEvent += OnNavIsOpenChanged;
        _themeState.IsDarkChangeEvent += OnThemeChanged;
    }

    protected override async Task OnInitializedAsync()
    {
        var authState = await _authState.GetAuthenticationStateAsync();
        var user = authState.User;
        _userName = user.GetUserName();
        _fullName = user.GetFullName();
    }
    private void OnNavIsOpenChanged()
    {
        InvokeAsync(StateHasChanged);
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
        _layoutState.NavIsOpenEvent -= OnNavIsOpenChanged;
        _themeState.IsDarkChangeEvent -= OnThemeChanged;
    }
}
