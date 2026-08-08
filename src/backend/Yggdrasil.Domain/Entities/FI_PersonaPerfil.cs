namespace Yggdrasil.Domain.Entities;

public class FI_PersonaPerfil
{
    public int PersonaId { get; set; }
    public int PerfilId { get; set; }

    public FI_Persona FI_Persona { get; set; } = null!;
    public FI_Perfil FI_Perfil { get; set; } = null!;

}
