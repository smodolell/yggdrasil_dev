using Yggdrasil.Module.Credito.CS.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.CS.Features.Operaciones.DTOs;

public class CreditoCSEditDto
{
    public int CreditoId { get; set; }
    public int TipoCreditoId { get; set; }
    public int PeriodicidadId { get; set; }
    public int EstatusCreditoId { get; set; }
    public int MetodoArmotizacionId { get; set; }

    public DateTime FechaInicio { get; set; }
    public DateTime FechaPrimeraRenta { get; set; }
    public DateTime? FechaFirmaContrato { get; set; }
    public DateTime? FechaActivacion { get; set; }
    public string ClaveCredito { get; set; } = string.Empty;

    public decimal Capital { get; set; }

    public decimal Tasa { get; set; }
    public decimal TasaIva { get; set; }
    public int Plazo { get; set; }
    public int VersionTabla { get; set; }

    public bool EsImportacionExcel => MetodoArmotizacionId == (int)AmortizationMethod.ImportExcel;
    public byte[] ExcelFileBytes { get; set; }
    public string NombreArchivoExcel { get; set; }
}

public class CreditoCSEditDtoValidator : AbstractValidator<CreditoCSEditDto>
{
    public CreditoCSEditDtoValidator()
    {

    }
}