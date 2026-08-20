using Yggdrasil.Module.Credito.CS.Features.Financial.Attibutes;
using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Amortization;

[AmortizationMethod(AmortizationMethod.American)]
public class AmericanaStrategy : AbstractAmortizationStrategy
{
    protected override string StrategyName => "Americano (Bullet con validación de Gracia)";

    public override Result<AmortizationResultDto> Calculate(AmortizationDto request, List<DateTime> fechas)
    {
        try
        {
            ValidarRequest(request);

            if (fechas == null || fechas.Count != request.Plazo)
            {
                return CrearResultadoError($"La lista de fechas de vencimiento debe contener exactamente {request.Plazo} elementos.");
            }

            // En el americano, no puede haber gracia que cubra la última cuota, 
            // ya que obligatoriamente se debe liquidar el capital en el vencimiento (Plazo).
            if (request.PeriodosGracia >= request.Plazo)
            {
                return CrearResultadoError("Los periodos de gracia de capital no pueden cubrir el plazo total en un esquema Americano.");
            }

            var resultados = new List<AmortizacionRow>();
            decimal saldoConstante = request.SaldoInicial;
            DateTime fechaReferenciaAnterior = request.FecInicioContrato;

            for (int i = 0; i < request.Plazo; i++)
            {
                int numeroPago = i + 1;
                DateTime fechaActual = fechas[i];

                int diasPeriodo = (fechaActual - fechaReferenciaAnterior).Days;

                decimal interesPeriodo = Math.Round(
                    saldoConstante * (decimal)((request.TasaAnual * diasPeriodo) / 360.0),
                    2, MidpointRounding.AwayFromZero);

                decimal ivaPeriodo = CalcularIVA(interesPeriodo, request.TasaIVA, request.GeneraIVAInteres);

                // En el Americano el capital siempre se paga en el último pago (independientemente de la gracia)
                bool esUltimoPago = (numeroPago == request.Plazo);
                decimal capital = esUltimoPago ? saldoConstante : 0m;
                decimal saldoFinal = esUltimoPago ? 0m : saldoConstante;

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

            return Result.Success(new AmortizationResultDto { TablaAmortiza = resultados });
        }
        catch (Exception ex)
        {
            return CrearResultadoError($"Error en estrategia Americana con Gracia: {ex.Message}");
        }
    }
}