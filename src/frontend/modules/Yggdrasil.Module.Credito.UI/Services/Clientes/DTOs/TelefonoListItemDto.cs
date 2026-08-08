namespace Yggdrasil.Module.Credito.UI.Services.Clientes.DTOs;

public class TelefonoListItemDto
{
    public int Id { get; set; }
    public DateTime FechaRegistro { get; set; }
    public int PersonaId { get; set; }
    public string Numero { get; set; } = string.Empty;

    public string Extension { get; set; } = string.Empty;
    public string InfoAdicional { get; set; } = string.Empty;


    public string NomTipoTelefono { get; set; } = string.Empty;
}
