namespace Yggdrasil.Domain.Entities;

public class SYS_RolAccessPoint
{
    public int Id { get; set; }

    public int RolId { get; set; }

    public Guid AccessPointId { get; set; }


    [ForeignKey(nameof(RolId))]
    public SYS_Rol SYS_Rol { get; set; } = null!;

    [ForeignKey(nameof(AccessPointId))]
    public SYS_AccessPoint SYS_AccessPoint { get; set; } = null!;
}