namespace Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;

public class PagoCapitalDto
{
    public Guid CreditoId { get; set; }
    public decimal Monto { get; set; }
    public DateTime FechaPago { get; set; }
}