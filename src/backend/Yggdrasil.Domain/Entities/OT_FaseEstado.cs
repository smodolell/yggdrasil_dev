namespace Yggdrasil.Domain.Entities;

public class OT_FaseEstado
{
    public int Id { get; set; }

    public int FaseId { get; set; }

    [Required]
    [MaxLength(80)]
    public string NomEstado { get; set; } = "";

    public bool Inicial { get; set; }
    public bool Edicion { get; set; }
    public bool Completado { get; set; }
    public bool Rechazado { get; set; }
    public bool Condicionado { get; set; }
    public bool Espera { get; set; }

    [ForeignKey(nameof(FaseId))]
    public OT_Fase OT_Fase { get; set; } = null!;
}
