namespace Yggdrasil.Module.Credito.Features.Clientes.DTOs;

public class DomicilioListItemDto
{
    public int Id { get; set; }
    public DateTime FechaRegistro { get; set; }
    public string NomTipoDomicilio { get; set; } = string.Empty;
    public int PersonaId { get; set; }
    public string Calle { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string NomLocalidad { get; set; } = string.Empty;
    public string NomDepartamento { get; set; } = string.Empty;
    public string NomProvincia { get; set; } = string.Empty;
    public string EntreCalles { get; set; } = string.Empty;
    public string YCalle { get; set; } = string.Empty;
}
