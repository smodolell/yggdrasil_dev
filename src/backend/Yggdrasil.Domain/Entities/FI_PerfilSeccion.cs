namespace Yggdrasil.Domain.Entities;

public class FI_PerfilSeccion
{
    public int PerfilId { get; set; }
    public int SeccionId { get; set; }

    public bool ActivoCreate { get; set; }
    public bool ActivoEdit { get; set; }
    public bool ActivoExtension { get; set; }

    public FI_Perfil FI_Perfil { get; set; } = null!;
    public FI_Seccion FI_Seccion { get; set; } = null!;
}
