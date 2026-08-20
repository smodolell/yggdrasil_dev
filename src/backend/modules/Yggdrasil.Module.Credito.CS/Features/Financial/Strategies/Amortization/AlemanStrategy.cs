using Yggdrasil.Module.Credito.CS.Features.Financial.Attibutes;
using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Amortization;

[AmortizationMethod(AmortizationMethod.German)]
public class AlemanStrategy : AbstractAmortizationStrategy
{
    protected override string StrategyName => "Alemán (Capital Constante con Gracia)";

    public override Result<AmortizationResultDto> Calculate(AmortizationDto request, List<DateTime> fechas)
    {
        try
        {
            ValidarRequest(request);

            if (fechas == null || fechas.Count != request.Plazo)
            {
                return CrearResultadoError($"La lista de fechas de vencimiento debe contener exactamente {request.Plazo} elementos.");
            }

            // 1. Validar periodos de gracia y calcular capital fijo para cuotas restantes
            int periodosAmortizables = request.Plazo - request.PeriodosGracia;
            if (periodosAmortizables <= 0)
            {
                return CrearResultadoError("Los periodos de gracia no pueden ser mayores o iguales al plazo total del crédito.");
            }

            decimal capitalFijoRecalculado = Math.Round(request.SaldoInicial / periodosAmortizables, 2, MidpointRounding.AwayFromZero);

            var resultados = new List<AmortizacionRow>();
            decimal saldoActual = request.SaldoInicial;
            decimal totalCapitalAmortizado = 0;
            DateTime fechaReferenciaAnterior = request.FecInicioContrato;

            for (int i = 0; i < request.Plazo; i++)
            {
                int numeroPago = i + 1;
                DateTime fechaActual = fechas[i];
                bool enPeriodoGracia = numeroPago <= request.PeriodosGracia;

                int diasPeriodo = (fechaActual - fechaReferenciaAnterior).Days;

                decimal interesPeriodo = Math.Round(
                    saldoActual * (decimal)((request.TasaAnual * diasPeriodo) / 360.0),
                    2, MidpointRounding.AwayFromZero);

                decimal ivaPeriodo = CalcularIVA(interesPeriodo, request.TasaIVA, request.GeneraIVAInteres);

                // CAPITAL
                decimal capital;
                if (enPeriodoGracia)
                {
                    capital = 0m;
                }
                else if (numeroPago == request.Plazo)
                {
                    capital = saldoActual; // Liquidar centavos y remanente al final
                }
                else
                {
                    capital = capitalFijoRecalculado;
                }

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

                saldoActual = saldoFinal;
                totalCapitalAmortizado += capital;
                fechaReferenciaAnterior = fechaActual;
            }

            // Ajuste final
            var diferenciaRedondeo = request.SaldoInicial - totalCapitalAmortizado;
            if (Math.Abs(diferenciaRedondeo) > 0.00m && resultados.Count > 0)
            {
                var ultimo = resultados.Last();
                ultimo.Capital += diferenciaRedondeo;
                ultimo.Total = ultimo.Capital + ultimo.Interes + ultimo.IVA;
                ultimo.SaldoFinal = 0;
            }

            return Result.Success(new AmortizationResultDto { TablaAmortiza = resultados });
        }
        catch (Exception ex)
        {
            return CrearResultadoError($"Error en estrategia Alemana con Gracia: {ex.Message}");
        }
    }
}