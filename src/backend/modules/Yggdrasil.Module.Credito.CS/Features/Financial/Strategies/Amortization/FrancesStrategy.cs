using Yggdrasil.Module.Credito.CS.Features.Financial.Attibutes;
using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Amortization;

[AmortizationMethod(AmortizationMethod.French)]
public class FrancesStrategy : AbstractAmortizationStrategy
{
    protected override string StrategyName => "Francés (Cuota Fija con Gracia)";

    public override Result<AmortizationResultDto> Calculate(AmortizationDto request, List<DateTime> fechas)
    {
        try
        {
            ValidarRequest(request);

            if (fechas == null || fechas.Count != request.Plazo)
            {
                return CrearResultadoError($"La lista de fechas de vencimiento debe contener exactamente {request.Plazo} elementos.");
            }

            var resultados = new List<AmortizacionRow>();
            decimal saldoActual = request.SaldoInicial;
            decimal totalCapitalAmortizado = 0;
            DateTime fechaReferenciaAnterior = request.FecInicioContrato;

            // 1. Determinar el plazo real de amortización (Plazo Total - Periodos de Gracia)
            int periodosAmortizables = request.Plazo - request.PeriodosGracia;
            if (periodosAmortizables <= 0)
            {
                return CrearResultadoError("Los periodos de gracia no pueden ser mayores o iguales al plazo total del crédito.");
            }

            // 2. Calcular la Cuota Fija (PMT) recalculada para los periodos amortizables restantes
            double tasaParaCalculo = request.TasaAnual * (1 + request.TasaIVA);

            // Creamos un DTO temporal con el plazo reducido para calcular el PMT real de amortización
            var requestTemporal = new AmortizationDto
            {
                SaldoInicial = request.SaldoInicial,
                TasaAnual = request.TasaAnual,
                TasaIVA = request.TasaIVA,
                UsaDias = request.UsaDias,
                ParamDias = request.ParamDias,
                ParamMes = request.ParamMes,
                Plazo = periodosAmortizables // Asignamos el plazo reducido
            };
            decimal cuotaTotalFijaAmortizable = CalcularPMTAct360(requestTemporal, tasaParaCalculo);

            // 3. Iteración de la tabla
            for (int i = 0; i < request.Plazo; i++)
            {
                int numeroPago = i + 1;
                DateTime fechaActual = fechas[i];
                bool enPeriodoGracia = numeroPago <= request.PeriodosGracia;

                int diasPeriodo = (fechaActual - fechaReferenciaAnterior).Days;

                // INTERÉS NETO (Act/360)
                decimal interesNeto = Math.Round(
                    saldoActual * (decimal)((request.TasaAnual * diasPeriodo) / 360.0),
                    2, MidpointRounding.AwayFromZero);

                // IVA SOBRE INTERÉS
                decimal ivaInteres = CalcularIVA(interesNeto, request.TasaIVA, request.GeneraIVAInteres);

                // CAPITAL
                decimal capital;
                if (enPeriodoGracia)
                {
                    capital = 0m; // En periodo de gracia no se amortiza capital
                }
                else if (numeroPago == request.Plazo)
                {
                    capital = saldoActual; // Último periodo liquida el saldo restante
                }
                else
                {
                    // Cuota fija menos cargas financieras
                    capital = Math.Round(
                        cuotaTotalFijaAmortizable - interesNeto - ivaInteres,
                        2, MidpointRounding.AwayFromZero);

                    if (capital < 0) capital = 0m;
                    if (capital > saldoActual) capital = saldoActual;
                }

                // TOTAL MENSUAL (Durante la gracia la cuota disminuye porque Capital = 0)
                decimal totalMensual = capital + interesNeto + ivaInteres;
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
                    Interes = interesNeto,
                    IVA = ivaInteres,
                    Total = totalMensual,
                    SaldoFinal = saldoFinal,
                    EsValorResidual = false
                });

                saldoActual = saldoFinal;
                totalCapitalAmortizado += capital;
                fechaReferenciaAnterior = fechaActual;
            }

            // 4. Ajuste de centavos
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
            return CrearResultadoError($"Error en estrategia Francesa con Gracia: {ex.Message}");
        }
    }
}