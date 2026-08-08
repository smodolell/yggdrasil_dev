namespace Yggdrasil.Domain.Entities;

public class SYS_Menu
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


    [MaxLength(1000)]
    public string? Icon { get; set; }

    [MaxLength(80)]
    public string Name { get; set; } = "";

    public int Order { get; set; }


    public ICollection<SYS_AccessPoint> SYS_AccessPoint { get; set; } = new HashSet<SYS_AccessPoint>();

}
