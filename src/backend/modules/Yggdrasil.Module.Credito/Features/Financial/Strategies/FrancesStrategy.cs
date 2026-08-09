using Yggdrasil.Module.Credito.Features.Financial.Attibutes;
using Yggdrasil.Module.Credito.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.Features.Financial.Strategies;

[AmortizationMethod(AmortizationMethod.French)]
public class FrancesStrategy : AbstractAmortizationStrategy
{
    protected override string StrategyName => "Francés (Cuota Fija)";

    public override Result<AmortizationResultDto> Calculate(AmortizationDto request)
    {
        try
        {
            ValidarRequest(request);

            var resultados = new List<AmortizacionRow>();
            decimal saldoActual = request.SaldoInicial;

            // 1. Generar calendario de fechas primero (esencial para Act/360)
            var fechasVencimiento = GenerarCalendarioFechas(request);

            // 2. Calcular Cuota Total Fija (PMT) incluyendo el IVA si el contrato lo requiere
            // Usamos la tasa anual "inflada" por IVA para el cálculo de la anualidad constante
            double tasaParaCalculo = request.TasaAnual * (1 + request.TasaIVA);
            decimal cuotaTotalFija = CalcularPMTAct360(tasaParaCalculo, request.Plazo, request.SaldoInicial);

            decimal totalCapitalAmortizado = 0;
            DateTime fechaReferenciaAnterior = request.FecInicioContrato;

            // 3. Iteración de la tabla
            for (int i = 0; i < request.Plazo; i++)
            {
                int numeroPago = i + 1;
                DateTime fechaActual = fechasVencimiento[i];

                // Calcular días reales del periodo actual
                int diasPeriodo = (fechaActual - fechaReferenciaAnterior).Days;

                // INTERÉS NETO: (Saldo * Tasa * Días) / 360
                decimal interesNeto = Math.Round(
                    saldoActual * (decimal)((request.TasaAnual * diasPeriodo) / 360.0),
                    2, MidpointRounding.AwayFromZero);

                // IVA SOBRE INTERÉS
                decimal ivaInteres = Math.Round(
                    interesNeto * (decimal)request.TasaIVA,
                    2, MidpointRounding.AwayFromZero);

                // CAPITAL: Cuota Fija - Interés Neto - IVA
                // En el último periodo, el capital es exactamente el saldo restante
                decimal capital;
                if (numeroPago == request.Plazo)
                {
                    capital = saldoActual;
                }
                else
                {
                    capital = Math.Round(
                        cuotaTotalFija - interesNeto - ivaInteres,
                        2, MidpointRounding.AwayFromZero);
                }

                // TOTAL REAL (puede variar ligeramente en el último pago por redondeos)
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

                // Preparar siguiente iteración
                saldoActual = saldoFinal;
                totalCapitalAmortizado += capital;
                fechaReferenciaAnterior = fechaActual;
            }

            // 4. Ajuste final por diferencias de redondeo en Capital
            var diferenciaRedondeo = request.SaldoInicial - totalCapitalAmortizado;
            if (Math.Abs(diferenciaRedondeo) > 0.00m) // Si hay cualquier diferencia de centavos
            {
                var ultimo = resultados.Last();
                ultimo.Capital += diferenciaRedondeo;
                ultimo.Total = ultimo.Capital + ultimo.Interes + ultimo.IVA;
                ultimo.SaldoFinal = 0;
            }


            return Result.Success(new AmortizationResultDto
            {
                TablaAmortiza = resultados,
            });
        }
        catch (Exception ex)
        {
            return CrearResultadoError($"Error en estrategia Francesa (Act/360): {ex.Message}");
        }
    }
}