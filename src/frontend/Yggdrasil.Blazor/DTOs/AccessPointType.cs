using System.Text.Json.Serialization;

namespace Yggdrasil.Blazor.DTOs;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AccessPointType
{
    LeftMenu,
    Page,
    Element
}