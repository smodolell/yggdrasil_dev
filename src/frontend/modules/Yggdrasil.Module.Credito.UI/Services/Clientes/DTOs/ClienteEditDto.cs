namespace Yggdrasil.Module.Credito.UI.Services.Clientes.DTOs;

public class ClienteEditDto
{
    public int PersonaId { get; set; }
    public int? TipoPersonaId { get; set; }
    public int? GeneroId { get; set; }
    public int? EdoCivilId { get; set; }
    public string? Nombre { get; set; } = "";
    public string? Apellido { get; set; } = "";
    public string? DNI { get; set; }
    public string? CUIT { get; set; }
    public string? RazonSocial { get; set; } = "";
    public DateTime? FechaConstitucion { get; set; }
    public DateTime? FechaNacimiento { get; set; }


    public int? PaisId { get; set; }
    public string? Email { get; set; }

    //public DateTime? FechaRegistro { get; set; }
}
