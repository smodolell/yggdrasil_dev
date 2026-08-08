namespace Yggdrasil.Module.Credito.Features.Configuracion.Perfil.DTOs;

public class SeccionListItemDto
{
    public int Id { get; set; }
    public string NomSeccion { get; set; } = string.Empty;
    public bool IsCreate { get; set; }
    public bool IsEdit { get; set; }
    public bool IsExtension { get; set; }
}
