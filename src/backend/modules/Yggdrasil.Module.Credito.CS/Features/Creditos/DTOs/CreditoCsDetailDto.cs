namespace Yggdrasil.Module.Credito.CS.Features.Creditos.DTOs;

public class CreditoCsDetailDto
{
    public int Id { get; set; }
    public string ClaveCredito { get; set; } = "";

    public int EstatusCreditoId { get; set; }
    public string NomEstatusCredito { get; set; } = "";

    public int TipoCreditoId { get; set; }
    public string NomTipoCredito { get; set; } = "";

    public int PeriodicidadId { get; set; }
    public string NomPeriodicidad { get; set; } = "";

    public int MetodoArmotizacionId { get; set; }
    public string NomMetodoArmotizacion { get; set; } = "";

    public DateTime FechaInicio { get; set; }
    public DateTime FechaPrimeraRenta { get; set; }
    public DateTime? FechaFirmaContrato { get; set; }
    public DateTime? FechaActivacion { get; set; }

    public decimal Capital { get; set; }
    public decimal Tasa { get; set; }
    public decimal TasaIva { get; set; }
    public int Plazo { get; set; }
}
