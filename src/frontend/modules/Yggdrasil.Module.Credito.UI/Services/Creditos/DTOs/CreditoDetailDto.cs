namespace Yggdrasil.Module.Credito.UI.Services.Creditos.DTOs;

public class CreditoDetailDto
{
    // ── Crédito ──────────────────────────────────────────────────────────────
    public int Id { get; set; }
    public string ClaveCredito { get; set; } = "";
    public int EstatusCreditoId { get; set; }
    public string NomEstatusCredito { get; set; } = "";
    public int VersionTabla { get; set; }

    public DateTime FechaRegistro { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaPrimeraRenta { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaActivacion { get; set; }
    public DateTime? FechaTerminacion { get; set; }

    public decimal Capital { get; set; }
    public decimal CapitalFinanciado { get; set; }
    public decimal PagoMensual { get; set; }
    public int Plazo { get; set; }

    public decimal Tasa { get; set; }
    public decimal PuntosMas { get; set; }
    public decimal PuntosPor { get; set; }
    public decimal TasaBase { get; set; }

    public decimal TasaMora { get; set; }
    public decimal PuntosMasMora { get; set; }
    public decimal PuntosPorMora { get; set; }
    public decimal TasaBaseMora { get; set; }

    public decimal TasaIva { get; set; }

    public int MonedaId { get; set; }
    public string NomMoneda { get; set; } = "";
    public string ClaveMoneda { get; set; } = "";

    public int PeriodicidadId { get; set; }
    public string NomPeriodicidad { get; set; } = "";

    // ── Producto ─────────────────────────────────────────────────────────────
    public int ProductoId { get; set; }
    public string ClaveProducto { get; set; } = "";
    public string NomProducto { get; set; } = "";

    // ── Cliente ──────────────────────────────────────────────────────────────
    public int PersonaId { get; set; }
    public string RFC { get; set; } = "";
    public string CURP { get; set; } = "";
    public string Email { get; set; } = "";
    public string NomCliente { get; set; } = "";
}
