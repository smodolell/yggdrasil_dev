namespace Yggdrasil.Module.Audit.Features.LoginLog.DTOs;

public class LoginLogDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public DateTime Time { get; set; }
    public string? Agent { get; set; }
    public string? Ip { get; set; }
    public bool IsSuccessd { get; set; }
}
