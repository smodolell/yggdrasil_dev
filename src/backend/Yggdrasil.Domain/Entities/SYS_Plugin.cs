namespace Yggdrasil.Domain.Entities;

public class SYS_Plugin
{
    public Guid Id { get; set; }

    public int ApplicationId { get; set; }

    [Required]
    [MaxLength(80)]
    public string PluginName { get; set; } = "";

    [Required]
    [MaxLength(500)]
    public string PluginDescription { get; set; } = "";

    public bool MenuGlobal { get; set; }


    public bool Active { get; set; }



    [ForeignKey(nameof(ApplicationId))]
    public SYS_Application SYS_Application { get; set; } = null!;

    public ICollection<SYS_AccessPoint> SYS_AccessPoint { get; set; } = new HashSet<SYS_AccessPoint>();
}



