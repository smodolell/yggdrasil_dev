namespace Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;

public class CreditoIntraDiaListItemDto
{
    public Guid Id { get; set; }

    public decimal MontoOtorgado { get; set; }

    public decimal Capital { get; set; }

    public decimal Tasa { get; set; }

    public decimal TasaIva { get; set; }

    public DateTime FechaPrimeraRenta { get; set; }

    public int Estado { get; set; }
}
