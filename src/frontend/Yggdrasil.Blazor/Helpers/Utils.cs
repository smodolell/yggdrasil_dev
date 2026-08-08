using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Yggdrasil.Blazor.DTOs;

namespace Yggdrasil.Blazor.Helpers;

public static class Utils
{
    private static readonly Dictionary<string, string> IconMap = new()
    {
        { "creditos", Icons.Material.Filled.CreditCard },
        { "mora", Icons.Material.Filled.Warning },
        { "recaudacion", Icons.Material.Filled.AttachMoney },
        { "currency", Icons.Material.Filled.CurrencyExchange },
        { "money", Icons.Material.Filled.Money },
        { "solicitudes", Icons.Material.Filled.Description },
        { "usuarios", Icons.Material.Filled.People }
    };

    public static string GetIcon(string iconName)
    {
        if (string.IsNullOrEmpty(iconName))
        {
            return Icons.Material.Filled.Info; // icono por defecto
        }

        return IconMap.TryGetValue(iconName.ToLower(), out var icon)
            ? icon
            : Icons.Material.Filled.Info; // icono por defecto
    }
    public static string GetIconByEstatus(bool estatus)
    {

        return estatus ? Icons.Material.Filled.Done : Icons.Material.Filled.Close;
    }

    public static Color GetColorByEstatus(bool estatus)
    {
        return estatus ? Color.Success : Color.Error;
    }

    public static List<SelectListItemDto> ToSelectList<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
                  .Cast<TEnum>()
                  .Select(e => new SelectListItemDto
                  {
                      Text = e.GetDisplayName(), // Necesitas implementar GetDisplayName()
                      Value = Convert.ToInt32(e).ToString(),
                  })
                  .ToList();
    }
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?
            .Name ?? enumValue.ToString();
    }
}
