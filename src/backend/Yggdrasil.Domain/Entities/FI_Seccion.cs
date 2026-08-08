namespace Yggdrasil.Domain.Entities;

public class FI_Seccion
{

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string NomSeccion { get; set; } = "";

    [Required]
    public int Orden { get; set; }


    public bool IsCreate { get; set; }
    public bool IsEdit { get; set; }
    public bool IsExtension { get; set; }

    public bool Activa { get; set; }
    public ICollection<FI_PerfilSeccion> FI_PerfilSeccion { get; set; } = new HashSet<FI_PerfilSeccion>();
}
