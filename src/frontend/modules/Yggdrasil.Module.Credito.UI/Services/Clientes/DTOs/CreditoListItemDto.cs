namespace Yggdrasil.Module.Credito.UI.Services.Clientes.DTOs;

public class CreditoListItemDto
{
    public int Id { get; set; }
    public int EstatusCreditoId { get; set; }
    public string ClaveCredito { get; set; } = "";
    public decimal Capital { get; set; }
    public decimal Tasa { get; set; }
    public decimal TasaIva { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaMinimaInteres { get; set; }
    public DateTime FechaActivacion { get; set; }

    public string NomEstatusCredito { get; set; } = string.Empty;
}
