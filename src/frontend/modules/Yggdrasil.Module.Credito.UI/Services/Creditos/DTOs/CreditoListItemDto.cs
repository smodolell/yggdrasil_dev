namespace Yggdrasil.Module.Credito.UI.Services.Creditos.DTOs;

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
    public string NomCliente { get; set; } = string.Empty;
}
