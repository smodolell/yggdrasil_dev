namespace Yggdrasil.Module.Credito.UI.Services.Clientes.DTOs;

public class PersonaListItemDto
{
    public int Id { get; set; }
    public int GeneroId { get; set; }
    public int EdoCivilId { get; set; }
    public string NomPerfil { get; set; } = string.Empty;
    public string LugarNacimientoId { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; }
    public string PrimerNombre { get; set; } = string.Empty;
    public string SegundoNombre { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string ApellidoMaterno { get; set; } = string.Empty;
    public string RazonSocial { get; set; } = string.Empty;
    public string RFC { get; set; } = string.Empty;
    public string CURP { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
    public DateTime FechaAltaCliente { get; set; }
    public string Email { get; set; } = string.Empty;



    public string NombreCliente => _nombreCliente();

    private string _nombreCliente()
    {
        var result = PrimerNombre;
        result += " " + SegundoNombre;
        result += " " + ApellidoPaterno;
        result += " " + ApellidoMaterno;
        return result.Trim();
    }
}
