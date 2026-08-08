namespace Yggdrasil.Domain.Entities;

public class SYS_LoginLog
{
    public int Id { get; set; }
    [MaxLength(80)]
    public string UserName { get; set; } = null!;
    public DateTime Time { get; set; }
    public string? Agent { get; set; }
    public string? Ip { get; set; }
    public bool IsSuccessd { get; set; }
}
