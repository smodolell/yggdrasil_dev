namespace Yggdrasil.Module.Credito.Features.Clientes.DTOs;

public class PersonaFisicaEditDto
{
    public int? PersonaId { get; set; }
    public int? GeneroId { get; set; }
    public int? EdoCivilId { get; set; }
    public string LugarNacimientoId { get; set; } = string.Empty;
    public DateTime? FechaRegistro { get; set; }
    public string? PrimerNombre { get; set; } = string.Empty;
    public string? SegundoNombre { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string ApellidoMaterno { get; set; } = string.Empty;
    public string RFC { get; set; } = string.Empty;
    public string CURP { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
    public string Email { get; set; } = string.Empty;
}
