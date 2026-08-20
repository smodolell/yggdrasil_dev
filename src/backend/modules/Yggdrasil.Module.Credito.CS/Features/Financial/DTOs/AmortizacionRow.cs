namespace Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;

public class AmortizacionRow
{
    public int NoPago { get; set; }
    public int IdTipoTabla { get; set; }  // 1 = detalle, 2 = consolidado
    public DateTime FecInicio { get; set; }
    public DateTime FecFinal { get; set; }
    public DateTime FecVencimiento { get; set; }
    public int Dias { get; set; }
    public decimal SaldoInicial { get; set; }
    public decimal Capital { get; set; }
    public decimal Interes { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public decimal SaldoFinal { get; set; }
    public bool EsValorResidual { get; set; }

    /// <summary>
    /// Porcentaje de capital amortizado en este periodo (ej. 1.3280%)
    /// </summary>
    public decimal PorcentajeAmortizacion { get; set; }

    /// <summary>
    /// Porcentaje de capital amortizado acumulado a la fecha
    /// </summary>
    public decimal PorcentajeAcumulado { get; set; }

    public override string ToString()
    {
        return $"Pago {NoPago}: Capital: {Capital:C2}, Interés: {Interes:C2}, IVA: {IVA:C2}, Total: {Total:C2}, Saldo: {SaldoFinal:C2}";
    }
}

