using Yggdrasil.Module.Credito.Features.Financial.Attibutes;
using Yggdrasil.Module.Credito.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.Features.Financial.Strategies;

[AmortizationMethod(AmortizationMethod.American)]
public class AmericanaStrategy : AbstractAmortizationStrategy
{
    protected override string StrategyName => "Americano (Intereses sobre Días Reales)";

    public override Result<AmortizationResultDto> Calculate(AmortizationDto request)
    {
        try
        {
            ValidarRequest(request);

            var resultados = new List<AmortizacionRow>();
            var saldoConstante = request.SaldoInicial;

            // 1. Generar calendario de fechas primero
            var fechasVencimiento = GenerarCalendarioFechas(request);
            DateTime fechaReferenciaAnterior = request.FecInicioContrato;

            for (int i = 0; i < request.Plazo; i++)
            {
                int numeroPago = i + 1;
                DateTime fechaActual = fechasVencimiento[i];

                // 2. Calcular días reales del periodo (Fundamental para Scotiabank)
                int diasPeriodo = (fechaActual - fechaReferenciaAnterior).Days;

                // 3. Interés sobre días reales: (Saldo * TasaAnual * Días) / 360
                decimal interesPeriodo = Math.Round(
                    saldoConstante * (decimal)((request.TasaAnual * diasPeriodo) / 360.0),
                    2, MidpointRounding.AwayFromZero);

                decimal ivaPeriodo = CalcularIVA(interesPeriodo, request.TasaIVA, request.GeneraIVAInteres);

                // En el Americano, el capital es 0 siempre, excepto el último mes
                decimal capital = (numeroPago == request.Plazo) ? saldoConstante : 0;
                decimal saldoFinal = (numeroPago == request.Plazo) ? 0 : saldoConstante;

                resultados.Add(new AmortizacionRow
                {
                    NoPago = numeroPago,
                    IdTipoTabla = 1,
                    FecInicio = fechaReferenciaAnterior,
                    FecVencimiento = fechaActual,
                    FecFinal = fechaActual,
                    Dias = diasPeriodo,
                    SaldoInicial = saldoConstante,
                    Capital = capital,
                    Interes = interesPeriodo,
                    IVA = ivaPeriodo,
                    Total = capital + interesPeriodo + ivaPeriodo,
                    SaldoFinal = saldoFinal,
                    EsValorResidual = false
                });

                fechaReferenciaAnterior = fechaActual;
            }

            return Result.Success(new AmortizationResultDto
            {
                TablaAmortiza = resultados,
            });
        }
        catch (Exception ex)
        {
            return CrearResultadoError($"Error en estrategia Americana (Act/360): {ex.Message}");
        }
    }
}