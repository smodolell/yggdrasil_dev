namespace Yggdrasil.Domain.Entities;

public class OT_SolicitudFase
{

    public int SolicitudId { get; set; }

    public int FaseId { get; set; }

    public int FaseEstadoId { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    [Required]
    public bool OK { get; set; } = false;



    [ForeignKey(nameof(FaseId))]
    public OT_Solicitud OT_Solicitud { get; set; } = null!;

    [ForeignKey(nameof(FaseId))]
    public OT_Fase OT_Fase { get; set; } = null!;

    [ForeignKey(nameof(FaseEstadoId))]
    public OT_FaseEstado OT_FaseEstado { get; set; } = null!;

}
