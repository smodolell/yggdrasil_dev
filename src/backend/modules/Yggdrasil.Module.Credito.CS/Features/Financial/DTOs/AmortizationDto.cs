namespace Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;

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

    /// <summary>
    /// Número de periodos/cuotas donde no se amortizará capital (solo se pagan intereses).
    /// </summary>
    public int PeriodosGracia { get; set; } = 0;


    public bool EsImportacionExcel { get; set; }
    public byte[] ExcelFileBytes { get; set; }
    public string NombreArchivoExcel { get; set; }

    public DateTime? FechaFirmaContrato { get; set; }

}



