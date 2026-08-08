namespace Yggdrasil.Module.Audit.UI.Services.Audits.DTOs;

public class AuditListItemDto
{
    public Guid Id { get; set; }
    public int AuditEventId { get; set; }
    public string Description { get; set; } = "";
    public DateTime RegisteredDate { get; set; }
    public string UserName { get; set; } = "";
    public bool HasError { get; set; }
    public string Message { get; set; } = "";
}
