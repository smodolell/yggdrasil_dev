namespace Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;

public class PagoIrregularDto
{
    public int NoPago { get; set; }
    public decimal Capital { get; set; }
    public DateTime FecVencimiento { get; set; }
    public bool NoAplicaCapital { get; set; }
}

