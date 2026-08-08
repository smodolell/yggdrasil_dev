namespace Yggdrasil.Module.Credito.UI.Services.Clientes.DTOs;

public class SeccionPersonaDto
{
    public int SeccionId { get; set; }
    public string NomSeccion { get; set; } = "";
    public bool IsCreate { get; set; }
    public bool IsEdit { get; set; }
    public bool IsExtension { get; set; }
}
