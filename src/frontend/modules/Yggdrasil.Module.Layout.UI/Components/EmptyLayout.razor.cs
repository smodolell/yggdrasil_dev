using Microsoft.AspNetCore.Components;

namespace Yggdrasil.Module.Layout.UI.Components;

public partial class EmptyLayout : IDisposable
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            _themeState.IsDarkChangeEvent += OnThemeChanged;
            _themeState.ThemeChangeEvent += OnThemeChanged;
            _themeState.LoadTheme();
        }
    }

    private void OnThemeChanged() => InvokeAsync(StateHasChanged);

    public void Dispose()
    {
        _themeState.IsDarkChangeEvent -= OnThemeChanged;
        _themeState.ThemeChangeEvent -= OnThemeChanged;
    }
}
