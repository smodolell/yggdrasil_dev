namespace Yggdrasil.Module.Credito.Features.Financial.DTOs;

public class AmortizationDto
{
    public decimal SaldoInicial { get; set; }
    public int Plazo { get; set; }
    public DateTime FecPrimeraRenta { get; set; }
    public DateTime FecInicioContrato { get; set; }
    public double TasaAnual { get; set; }         // Para método Americano
    public double TasaIVA { get; set; }
    public bool GeneraIVAInteres => TasaIVA > 0;
    public bool UsaDias { get; set; }
    public int ParamDias { get; set; }
    public int ParamMes { get; set; }

  
}



