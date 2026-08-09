namespace Yggdrasil.Module.Credito.UI.Services.Clientes.DTOs;

public class ClienteEditDto
{
    public int PersonaId { get; set; }
    public int? TipoPersonaId { get; set; }
    public int? GeneroId { get; set; }
    public int? EdoCivilId { get; set; }
    public string PrimerNombre { get; set; } = "";
    public string SegundoNombre { get; set; } = "";
    public string ApellidoPaterno { get; set; } = "";
    public string ApellidoMaterno { get; set; } = "";
    public string RFC { get; set; } = "";
    public string CURP { get; set; } = "";
    public string NSS { get; set; } = "";
    public string? RazonSocial { get; set; } = "";
    public DateTime? FechaConstitucion { get; set; }
    public DateTime? FechaNacimiento { get; set; }


    public int? PaisId { get; set; }
    public string? Email { get; set; }

    //public DateTime? FechaRegistro { get; set; }
}
