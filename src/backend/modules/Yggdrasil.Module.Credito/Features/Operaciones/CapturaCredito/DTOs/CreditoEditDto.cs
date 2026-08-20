using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Yggdrasil.Module.Credito.Features.Operaciones.CapturaCredito.DTOs;

public class CreditoEditDto
{

    public int PersonaId { get; set; }
    public int EstatusCreditoId { get; set; }
    public int ProductoId { get; set; }
    public int? MonedaId { get; set; }
    public int? PeriodicidadId { get; set; }
    public int TipoTablaAmortizaId { get; set; }
    public decimal Capital { get; set; }
    public decimal CapitalFinanciado { get; set; }
    public decimal Tasa { get; set; }
    public decimal PuntosMas { get; set; } = 0;
    public decimal PuntosPor { get; set; } = 1;
    public decimal TasaBase { get; set; }

    public decimal TasaMora { get; set; }
    public decimal PuntosMasMora { get; set; }
    public decimal PuntosPorMora { get; set; } = 1;
    public decimal TasaBaseMora { get; set; }

    public decimal? TasaIva { get; set; }
    public int? Plazo { get; set; }

    public DateTime? FechaRegistro { get; set; }
    public DateTime? FechaAlta { get; set; }
    public DateTime? FechaInicio { get; set; }
















    public string NomPeriodicidad { get; set; } = "";

    public int TasaId { get; set; }



    public DateTime? FechaPrimeraRenta { get; set; }



    





}
