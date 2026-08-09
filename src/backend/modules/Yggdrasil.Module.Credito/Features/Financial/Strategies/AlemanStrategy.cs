using Yggdrasil.Module.Credito.Features.Financial.Attibutes;
using Yggdrasil.Module.Credito.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.Features.Financial.Strategies;

[AmortizationMethod(AmortizationMethod.German)]
public class AlemanStrategy : AbstractAmortizationStrategy
{
    protected override string StrategyName => "Alemán (Capital Constante)";

    public override Result<AmortizationResultDto> Calculate(AmortizationDto request)
    {
        try
        {
            ValidarRequest(request);

            var resultados = new List<AmortizacionRow>();
            decimal saldoActual = request.SaldoInicial;

            // 1. Amortización de capital constante: Saldo / Plazo
            decimal capitalFijo = Math.Round(request.SaldoInicial / request.Plazo, 2, MidpointRounding.AwayFromZero);

            // 2. Generar calendario de fechas antes del cálculo
            var fechasVencimiento = GenerarCalendarioFechas(request);
            DateTime fechaReferenciaAnterior = request.FecInicioContrato;

            for (int i = 0; i < request.Plazo; i++)
            {
                int numeroPago = i + 1;
                DateTime fechaActual = fechasVencimiento[i];

                // 3. Obtener días reales del periodo (Fundamental para Santander)
                int diasPeriodo = (fechaActual - fechaReferenciaAnterior).Days;

                // 4. Interés sobre días reales: (Saldo * TasaAnual * Días) / 360
                decimal interesPeriodo = Math.Round(
                    saldoActual * (decimal)((request.TasaAnual * diasPeriodo) / 360.0),
                    2, MidpointRounding.AwayFromZero);

                decimal ivaPeriodo = CalcularIVA(interesPeriodo, request.TasaIVA, request.GeneraIVAInteres);

                // 5. Ajuste de capital para el último pago
                decimal capital = (numeroPago == request.Plazo) ? saldoActual : capitalFijo;

                decimal totalMensual = capital + interesPeriodo + ivaPeriodo;
                decimal saldoFinal = Math.Round(saldoActual - capital, 2, MidpointRounding.AwayFromZero);

                resultados.Add(new AmortizacionRow
                {
                    NoPago = numeroPago,
                    IdTipoTabla = 1,
                    FecInicio = fechaReferenciaAnterior,
                    FecVencimiento = fechaActual,
                    FecFinal = fechaActual,
                    Dias = diasPeriodo,
                    SaldoInicial = saldoActual,
                    Capital = capital,
                    Interes = interesPeriodo,
                    IVA = ivaPeriodo,
                    Total = totalMensual,
                    SaldoFinal = saldoFinal,
                    EsValorResidual = false
                });

                // Preparar siguiente ciclo
                saldoActual = saldoFinal;
                fechaReferenciaAnterior = fechaActual;
            }

            return Result.Success(new AmortizationResultDto
            {
                TablaAmortiza = resultados,                
            });
        }
        catch (Exception ex)
        {
            return CrearResultadoError($"Error en estrategia Alemana (Act/360): {ex.Message}");
        }
    }
}