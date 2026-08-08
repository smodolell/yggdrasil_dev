namespace Yggdrasil.Module.Credito.UI.Services.Configuacion.DTOs;

public class PerfilEditDto
{
    public int? PerfilId { get; set; }
    public string NomPerfil { get; set; } = string.Empty;

    public List<SeccionEditDto> Items { get; set; } = new List<SeccionEditDto>();
    public bool Activo { get; set; }
}
