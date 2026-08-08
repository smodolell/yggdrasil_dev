namespace Yggdrasil.Blazor.DTOs;

public class PageDto
{
    public string Menu { get; set; } = "";
    public string MenuIcon { get; set; } = "";
    public string MenuItem { get; set; } = "";
    public string Route { get; set; } = "";
    public bool IsAnonymous { get; set; }
    public AccessPointType AccessPointType { get; set; }
}
