namespace Yggdrasil.Module.Credito.Features.Financial.DTOs;

public class AnticipoInteresDto
{
    public DateTime FecVencimiento { get; set; }
    public DateTime FecAnticipo { get; set; }
    public decimal MontoAnticipo { get; set; }
    public decimal InteresCalculado { get; set; }
    public decimal SaldoInteres { get; set; }
}

