namespace Yggdrasil.Module.Credito.Features.Creditos.DTOs;
public class CreditoListItemDto
{
    public int Id { get; set; }
    public int EstatusCreditoId { get; set; }
    public string ClaveCredito { get; set; } = string.Empty;
    public string NomProducto { get; set; } = string.Empty;
    public decimal Capital { get; set; }
    public decimal Tasa { get; set; }
    public decimal TasaIva { get; set; }

    public int Plazo { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaActivacion { get; set; }

    public string NomEstatusCredito { get; set; } = string.Empty;
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string RazonSocial { get; set; } = "";
    public string DNI { get; set; } = "";
    public string CUIT { get; set; } = "";

    public string NomCliente => _nomCliente();

    private string _nomCliente()
    {
        var result = Nombre;
        result += " " + Apellido;
        result += " " + RazonSocial;
        return result.Trim();
    }
}
