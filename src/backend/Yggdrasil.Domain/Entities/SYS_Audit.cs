namespace Yggdrasil.Domain.Entities;

public class SYS_Audit
{
    public Guid Id { get; set; }
    public int AuditEventId { get; set; }

    [Required]
    public DateTime RegisteredDate { get; set; }

    [Required]
    [MaxLength(60)]
    public string UserName { get; set; } = "";

    public bool HasError { get; set; }


    [Required]
    public string Message { get; set; } = "";


    [ForeignKey(nameof(AuditEventId))]
    public SYS_AuditEvent SYS_AuditEvent { get; set; } = null!;


}
