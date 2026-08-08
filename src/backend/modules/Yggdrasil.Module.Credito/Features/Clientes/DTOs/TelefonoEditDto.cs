namespace Yggdrasil.Module.Credito.Features.Clientes.DTOs;

public class TelefonoEditDto
{
    public int? TelefonoId { get; set; }
    public DateTime? FechaRegistro { get; set; }
    public int? TipoTelefonoId { get; set; }
    public int PersonaId { get; set; }
    public string Numero { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string InfoAdicional { get; set; } = string.Empty;
}
