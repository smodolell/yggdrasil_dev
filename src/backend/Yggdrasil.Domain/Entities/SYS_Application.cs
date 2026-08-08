namespace Yggdrasil.Domain.Entities;

public class SYS_Application
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string ApplicationName { get; set; } = "";

}
