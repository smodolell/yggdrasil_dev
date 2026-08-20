namespace Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;

public class NewDisposicionDto
{
    public Guid CreditoId { get; set; }
    public decimal Monto { get; set; }
    public DateTime FechaDisposicion { get; set; }
}