using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Amortization;

public abstract class AbstractAmortizationStrategy : IAmortizationStrategy
{
    protected abstract string StrategyName { get; }

    public abstract Result<AmortizationResultDto> Calculate(AmortizationDto request, List<DateTime> fechas);

    #region Métodos de ayuda comunes

    protected static decimal CalcularIVA(decimal baseCalculo, double tasaIVA, bool generaIVA)
    {
        if (!generaIVA || tasaIVA <= 0) return 0;
        return Math.Round(baseCalculo * (decimal)tasaIVA, 2, MidpointRounding.AwayFromZero);
    }

    protected static void ValidarRequest(AmortizationDto request)
    {
        if (request.SaldoInicial <= 0)
            throw new ArgumentException("El saldo inicial debe ser mayor a 0");

        if (request.Plazo <= 0)
            throw new ArgumentException("El plazo debe ser mayor a 0");

        if (request.FecPrimeraRenta <= request.FecInicioContrato)
            throw new ArgumentException("La fecha de primera renta debe ser posterior a la fecha de inicio");

        if (request.TasaAnual < 0)
            throw new ArgumentException("La tasa anual no puede ser negativa");
    }

    protected Result<AmortizationResultDto> CrearResultadoError(string mensaje)
    {
        return Result.Invalid(new ValidationError(mensaje));
    }

    protected Result<AmortizationResultDto> CrearResultadoError(params ValidationError[] errores)
    {
        return Result.Invalid(errores);
    }






    #endregion

    protected static decimal CalcularPMT(double tasa, int periodos, decimal valorPresente, decimal valorFuturo, int tipo)
    {
        if (tasa == 0) return (valorPresente - valorFuturo) / periodos;

        var rate = (decimal)tasa;
        var pow = (decimal)Math.Pow(1 + tasa, periodos);
        var factor = rate * pow / (pow - 1);

        var pmt = valorPresente * factor + valorFuturo * (rate / (pow - 1));

        return Math.Round(pmt, 2, MidpointRounding.AwayFromZero);
    }


    /// <summary>
    /// Calcula la cuota nivelada para base Act/360 adaptándose dinámicamente
    /// a esquemas Mensuales, Semanales o Quincenales.
    /// </summary>
    protected static decimal CalcularPMTAct360(AmortizationDto request, double tasaAnual)
    {
        if (tasaAnual == 0) return Math.Round(request.SaldoInicial / request.Plazo, 2);

        double diasPeriodoProyectados;

        if (request.UsaDias)
        {
            // Para Semanal (7) o Quincenal (15), usamos los días exactos de la periodicidad
            diasPeriodoProyectados = request.ParamDias;
        }
        else
        {
            // Para Mensual, se usa el estándar de días promedio por mes (365/12) por el número de meses
            double diasPromedioMes = 365.0 / 12.0;
            diasPeriodoProyectados = diasPromedioMes * request.ParamMes;
        }

        // Tasa periódica proyectada según días del período
        double tasaPeriodicaProyectada = (tasaAnual * diasPeriodoProyectados) / 360.0;

        return CalcularPMT(tasaPeriodicaProyectada, request.Plazo, request.SaldoInicial, 0, 0);
    }
}