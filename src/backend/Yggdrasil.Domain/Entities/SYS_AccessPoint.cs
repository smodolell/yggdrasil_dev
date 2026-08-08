namespace Yggdrasil.Domain.Entities;

public class SYS_AccessPoint
{
    public Guid Id { get; set; }
    public int AccessPointTypeId { get; set; }
    public int MenuId { get; set; }
    public Guid PluginId { get; set; }
    public int ApplicationId { get; set; }

    public string AccessPointName { get; set; } = "";

    [MaxLength(500)]
    public string? Icon { get; set; }

    public string Route { get; set; } = "";

    public string? PageElementId { get; set; }
    public string DescPageElement { get; set; } = "";


    public int Order { get; set; }

    //public bool IsClient { get; set; }

    public bool IsAnonymous { get; set; }

    [ForeignKey(nameof(MenuId))]
    public SYS_Menu SYS_Menu { get; set; } = null!;


    [ForeignKey(nameof(PluginId))]
    public SYS_Plugin SYS_Plugin { get; set; } = null!;



    [ForeignKey(nameof(AccessPointTypeId))]
    public SYS_AccessPointType SYS_AccessPointType { get; set; } = null!;

    public ICollection<SYS_RolAccessPoint> SYS_RolAccessPoint { get; set; } = new HashSet<SYS_RolAccessPoint>();
}
