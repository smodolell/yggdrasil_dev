using Yggdrasil.Module.Credito.Features.Financial.DTOs;

namespace Yggdrasil.Module.Credito.Features.Financial.Strategies;

public abstract class AbstractAmortizationStrategy : IAmortizationStrategy
{
    protected abstract string StrategyName { get; }

    public abstract Result<AmortizationResultDto> Calculate(AmortizationDto request);

    #region Métodos de ayuda comunes

    protected static List<AmortizacionRow> ActualizarFechas(List<AmortizacionRow> tabla, AmortizationDto request)
    {
        for (int i = 0; i < tabla.Count; i++)
        {
            var row = tabla[i];

            if (request.UsaDias)
            {
                row.FecVencimiento = request.FecPrimeraRenta.AddDays(request.ParamDias * i);
                row.FecFinal = row.FecVencimiento;
            }
            else
            {
                row.FecVencimiento = request.FecPrimeraRenta.AddMonths(request.ParamMes * i);

                // Ajuste para fin de mes
                while (row.FecVencimiento.Month == row.FecVencimiento.AddDays(1).Month)
                {
                    row.FecVencimiento = row.FecVencimiento.AddDays(1);
                }

                row.FecFinal = row.FecVencimiento;
            }
        }

        // Actualizar fechas de inicio
        if (tabla.Any())
        {
            tabla[0].FecInicio = request.FecInicioContrato;

            for (int i = 1; i < tabla.Count; i++)
            {
                tabla[i].FecInicio = tabla[i - 1].FecFinal.AddDays(1);
            }
        }

        return tabla;
    }

    protected static List<AmortizacionRow> CalcularDias(List<AmortizacionRow> tabla)
    {
        foreach (var row in tabla)
        {
            row.Dias = (row.FecFinal - row.FecInicio).Days + 1;
        }
        return tabla;
    }

    protected static decimal CalcularPMT(double tasa, int periodos, decimal valorPresente, decimal valorFuturo, int tipo)
    {
        if (tasa == 0) return (valorPresente - valorFuturo) / periodos;

        var rate = (decimal)tasa;
        var pow = (decimal)Math.Pow(1 + tasa, periodos);
        var factor = rate * pow / (pow - 1);

        // ✅ Corrección: Usar valorPresente directamente (no negativo)
        var pmt = valorPresente * factor + valorFuturo * (rate / (pow - 1));

        // Redondear a 2 decimales
        return Math.Round(pmt, 2, MidpointRounding.AwayFromZero);
    }

    protected static decimal CalcularPMTArrendamiento(double tasa, int periodos, decimal valorPresente, decimal valorFuturo)
    {
        if (tasa == 0) return (valorPresente - valorFuturo) / periodos;

        var rate = (decimal)tasa;
        var pow = (decimal)Math.Pow(1 + tasa, periodos);
        var factor = rate * pow / (pow - 1);

        // PV - FV / (1+r)^n
        var fvDiscount = valorFuturo / pow;
        var pmt = (valorPresente - fvDiscount) * factor;

        return Math.Round(pmt, 2, MidpointRounding.AwayFromZero);
    }

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

    /// <summary>
    /// Convierte tasa anual a tasa mensual efectiva
    /// </summary>
    /// <param name="tasaAnual">Tasa anual (ej: 0.0863 para 8.63%)</param>
    /// <param name="esEfectiva">true: efectiva, false: nominal</param>
    protected static double ConvertirTasaAnualAMensual(double tasaAnual, bool esEfectiva = true)
    {
        if (tasaAnual <= 0) return 0;

        if (esEfectiva)
        {
            // Tasa efectiva: (1 + TA)^(1/12) - 1
            return Math.Pow(1 + tasaAnual, 1.0 / 12.0) - 1;
        }
        else
        {
            // Tasa nominal: TA / 12
            return tasaAnual / 12.0;
        }
    }

    /// <summary>
    /// Convierte tasa anual a tasa efectiva para cualquier número de pagos por año
    /// </summary>
    /// <param name="tasaAnual">Tasa anual (ej: 0.0863 para 8.63%)</param>
    /// <param name="numeroDePagosPorAnio">Número de pagos por año (12 para mensual, 4 para trimestral, etc.)</param>
    protected static double ConvertirTasaAnualAEfectiva(double tasaAnual, int numeroDePagosPorAnio)
    {
        if (tasaAnual <= 0) return 0;
        if (numeroDePagosPorAnio <= 0) return 0;

        // Tasa efectiva: (1 + TA)^(1/n) - 1
        return Math.Pow(1 + tasaAnual, 1.0 / numeroDePagosPorAnio) - 1;
    }

    protected static double AjustarTasaConIva(double tasaMensual, double tasaIva, bool aplica)
    {
        return aplica ? tasaMensual * (1 + tasaIva) : tasaMensual;
    }



    #endregion

    /// <summary>
    /// Genera las fechas de vencimiento antes de calcular la amortización.
    /// Esto es vital para estrategias que usan días reales para el interés.
    /// </summary>
    protected static List<DateTime> GenerarCalendarioFechas(AmortizationDto request)
    {
        var fechas = new List<DateTime>();
        for (int i = 0; i < request.Plazo; i++)
        {
            if (request.UsaDias)
            {
                fechas.Add(request.FecPrimeraRenta.AddDays(request.ParamDias * i));
            }
            else
            {
                // Lógica de meses con ajuste de fin de mes
                var fecha = request.FecPrimeraRenta.AddMonths(i);
                // Si la fecha original era fin de mes, mantenemos fin de mes
                if (request.FecPrimeraRenta.Day >= 28 && fecha.AddDays(1).Month != fecha.Month)
                {
                    // Ya es fin de mes, no hacemos nada o ajustamos según política
                }
                fechas.Add(fecha);
            }
        }
        return fechas;
    }

    /// <summary>
    /// Calcula la cuota nivelada para base Act/360 usando días promedio (30.4166)
    /// </summary>
    protected static decimal CalcularPMTAct360(double tasaAnual, int periodos, decimal valorPresente)
    {
        if (tasaAnual == 0) return Math.Round(valorPresente / periodos, 2);

        // 365 días / 12 meses = 30.41666... días promedio por mes
        double diasPromedioMes = 365.0 / 12.0;
        
        // Tasa efectiva mensual proyectada: (Tasa Anual * Días Promedio) / 360
        double tasaMensualProyectada = (tasaAnual * diasPromedioMes) / 360.0;

        return CalcularPMT(tasaMensualProyectada, periodos, valorPresente, 0, 0);
    }
}