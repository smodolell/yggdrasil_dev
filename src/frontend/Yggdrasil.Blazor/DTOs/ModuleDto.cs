namespace Yggdrasil.Blazor.DTOs;

public class ModuleDto
{
    public Guid Id { get; set; }
    public string PluginName { get; set; } = "";
    public string Description { get; set; } = "";
    public List<PageDto> Pages { get; set; } = new List<PageDto>();



}
