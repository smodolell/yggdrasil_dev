using BitzArt.Blazor.Cookies;
using MudBlazor;
using MudBlazor.Utilities;

namespace Yggdrasil.Module.Layout.UI.Components.States;

public class ThemeState
{
    private readonly ICookieService _cookieService;
    private bool _isDark;
    private MudTheme _theme = new();

    public event Action? ThemeChangeEvent;
    public event Action? IsDarkChangeEvent;

    public ThemeState(ICookieService cookieService)
    {
        _cookieService = cookieService;
        SetupBaseTheme();
    }

    public async Task InitializeAsync()
    {
        var isDarkCookie = await _cookieService.GetAsync<string>("IsDark");
        _isDark = !string.IsNullOrEmpty(isDarkCookie.Value) && bool.Parse(isDarkCookie.Value);

        var primaryColorCookie = await _cookieService.GetAsync<string>("PrimaryColor");
        // Usamos un azul corporativo interactivo por defecto si no hay cookie
        // Esto garantiza que los links y botones siempre sean visibles
        var primaryColor = string.IsNullOrEmpty(primaryColorCookie.Value) ? "#0958d9" : primaryColorCookie.Value;

        UpdatePaletteColor(new MudColor(primaryColor));

        ThemeStateChanged();
        IsDarkStateChanged();
    }

    private void SetupBaseTheme()
    {
        _theme = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                // Colores interactivos
                Primary = "#0958d9",          // Azul financiero brillante (resuelve los links negros)
                Secondary = "#475569",        // Gris pizarra para acciones secundarias

                // Semántica financiera
                Success = "#16a34a",          // Verde estándar (saldos a favor)
                Warning = "#f59e0b",          // Ámbar (alertas)
                Error = "#dc2626",            // Rojo (moras, deudas, saldos negativos)

                // Fondos y Superficies
                Background = "#f1f5f9",       // Gris muy sutil para alto contraste sin fatigar la vista
                Surface = "#ffffff",
                AppbarBackground = "#0f172a", // Azul marino oscuro para el TopBar (mantiene el estilo corporativo)

                // Textos
                TextPrimary = "#334155",      // Gris carbón (letra más fina a la vista que el negro puro)
                TextSecondary = "#64748b",
                LinesDefault = "#e2e8f0"
            },
            LayoutProperties = new LayoutProperties()
            {
                DefaultBorderRadius = "4px", // Bordes más sobrios y rectos, menos redondeados
                DrawerWidthLeft = "250px",
                AppbarHeight = "60px"
            },
            Typography = new Typography()
            {
                Default = new DefaultTypography()
                {
                    // Inter y Segoe UI son ideales para alta densidad de datos
                    FontFamily = new[] { "Inter", "Segoe UI", "Helvetica Neue", "Arial", "sans-serif" },
                    FontSize = "0.875rem", // 14px - Letra más chica
                    FontWeight = "400",    // Letra más fina
                    LineHeight = "1.43",
                    LetterSpacing = "0.01071em"
                },
                H1 = new H1Typography() { FontSize = "1.5rem", FontWeight = "600" },
                H2 = new H2Typography() { FontSize = "1.25rem", FontWeight = "600" },
                H3 = new H3Typography() { FontSize = "1.125rem", FontWeight = "600" },
                H4 = new H4Typography() { FontSize = "1rem", FontWeight = "600" },
                H5 = new H5Typography() { FontSize = "0.875rem", FontWeight = "600" },
                H6 = new H6Typography() { FontSize = "0.75rem", FontWeight = "600" },
                Button = new ButtonTypography()
                {
                    FontSize = "0.875rem",
                    FontWeight = "500",
                    TextTransform = "none" // Quita las mayúsculas automáticas para un diseño más limpio
                }
            }
        };
    }

    private async Task SetCookieAsync(string key, string value, int days = 30)
    {
        try
        {
            await _cookieService.SetAsync(
                key: key,
                value: value,
                expiration: DateTimeOffset.Now.AddDays(days),
                httpOnly: false,
                secure: false,
                sameSiteMode: BitzArt.Blazor.Cookies.SameSiteMode.Lax
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting cookie: {ex.Message}");
        }
    }

    private void UpdatePaletteColor(MudColor color)
    {
        _theme.PaletteLight.Primary = color;
        _theme.PaletteLight.PrimaryDarken = color.ColorRgbDarken().ToString(MudColorOutputFormats.RGB);
        _theme.PaletteLight.PrimaryLighten = color.ColorRgbLighten().ToString(MudColorOutputFormats.RGB);

        _theme.PaletteDark.Primary = color;
        _theme.PaletteDark.PrimaryDarken = color.ColorRgbDarken().ToString(MudColorOutputFormats.RGB);
        _theme.PaletteDark.PrimaryLighten = color.ColorRgbLighten().ToString(MudColorOutputFormats.RGB);
    }

    public void LoadTheme()
    {
        IsDarkStateChanged();
        ThemeStateChanged();
    }

    public bool IsDark
    {
        get => _isDark;
        set
        {
            _isDark = value;
            _ = SetCookieAsync("IsDark", value.ToString());
            IsDarkStateChanged();
        }
    }

    public MudColor PrimaryColor
    {
        get => _theme.PaletteLight.Primary;
        set
        {
            UpdatePaletteColor(value);
            _ = SetCookieAsync("PrimaryColor", value.Value);
            ThemeStateChanged();
        }
    }

    public MudTheme MudTheme => _theme;

    private void ThemeStateChanged() => ThemeChangeEvent?.Invoke();
    private void IsDarkStateChanged() => IsDarkChangeEvent?.Invoke();
}