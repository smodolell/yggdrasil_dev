using Yggdrasil.Module.Credito.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.Features.Financial.DTOs;

public class AmortizationResultDto
{

    public AmortizationMethod Method { get; set; }
    public decimal SaldoInicial { get; set; }
    public double TasaAnual { get; set; }
    public int Plazo { get; set; }
    public decimal TasaIVA { get; set; }
    public bool GeneraIVAInteres { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FecPrimeraRenta { get; set; }

    public List<AmortizacionRow> TablaAmortiza { get; set; } = new();
    public decimal TotalCapital => TablaAmortiza.Sum(r => r.Capital);
    public decimal TotalInteres => TablaAmortiza.Sum(r => r.Interes);
    public decimal TotalIVA => TablaAmortiza.Sum(r => r.IVA);
    public decimal TotalPagado => TablaAmortiza.Sum(r => r.Total);
}

