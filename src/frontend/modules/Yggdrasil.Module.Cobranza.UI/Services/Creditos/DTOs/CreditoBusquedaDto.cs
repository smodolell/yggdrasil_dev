namespace Yggdrasil.Module.Cobranza.UI.Services.Creditos.DTOs;

public class CreditoBusquedaDto
{
    public int Id { get; set; }
    public int EstatusCreditoId { get; set; }
    public string ClaveCredito { get; set; } = "";
    public string NomCliente { get; set; } = "";
    public decimal Capital { get; set; }
    public DateTime? FechaActivacion { get; set; }
    public string NomEstatusCredito { get; set; } = "";
}
