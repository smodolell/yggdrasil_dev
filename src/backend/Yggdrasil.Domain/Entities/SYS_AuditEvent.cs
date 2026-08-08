namespace Yggdrasil.Domain.Entities;

public class SYS_AuditEvent
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Description { get; set; } = "";

}
