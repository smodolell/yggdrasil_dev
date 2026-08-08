namespace Yggdrasil.Domain.Entities;

public class SYS_AccessPointType
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string AccessPointTypeName { get; set; } = "";
    // Puede ser Page o ElementPage
}
