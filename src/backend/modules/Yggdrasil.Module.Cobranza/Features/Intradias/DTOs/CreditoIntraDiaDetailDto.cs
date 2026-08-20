namespace Yggdrasil.Module.Cobranza.Features.Intradias.DTOs;

public class CreditoIntraDiaDetailDto
{
    public Guid Id { get; set; }

    public decimal MontoOtorgado { get; set; }

    public decimal Capital { get; set; }

    public decimal Tasa { get; set; }

    public decimal TasaIva { get; set; }

    public DateTime FechaPrimeraRenta { get; set; }

    public int Estado { get; set; }

    public List<MovimientoIntraDiaDto> Movimientos { get; set; } = new List<MovimientoIntraDiaDto>();

    public List<InteresAcumuladoDto> InteresesAcumulados { get; set; } = new List<InteresAcumuladoDto>();
}

public class MovimientoIntraDiaDto
{
    public Guid Id { get; set; }
    public Guid CreditoId { get; set; }
    public int Nro { get; set; }
    public string Concepto { get; set; } = "";
    public DateTime Fecha { get; set; }
    public decimal Capital { get; set; }
    public decimal Interes { get; set; }
    public decimal Iva { get; set; }
    public decimal SaldoInsolutoResultante { get; set; }
    public DateTime FechaRegistro { get; set; }
}

public class InteresAcumuladoDto
{
    public Guid Id { get; set; }
    public Guid CreditoId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaCalculo { get; set; }
    public decimal SaldoCapital { get; set; }
    public int Dias { get; set; }
    public decimal Tasa { get; set; }
    public decimal TasaIva { get; set; }
    public decimal Interes { get; set; }
    public decimal Iva { get; set; }
    public decimal SaldoInsoluto { get; set; }
    public DateTime FechaRegistro { get; set; }
}
