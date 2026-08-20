using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Financial.Services;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Fechas;

public class SiguienteDiaHabilBBVAStrategies(ICalendarioLaboralService calendarioLaboral) : IFechasStrategies
{
    private readonly ICalendarioLaboralService _calendarioLaboral = calendarioLaboral;

    public async Task<List<DateTime>> GenerarCalendarioFechasAsync(AmortizationDto amortization)
    {
        var fechas = _calendarioLaboral.GenerarCalendarioFechasAsync(
            amortization.FecPrimeraRenta,
            amortization.UsaDias,
            amortization.ParamDias,
            amortization.ParamMes,
            amortization.Plazo
        );


        int diaCorteObjetivo = amortization.FecPrimeraRenta.Day;

        for (int i = 0; i < fechas.Count; i++)
        {
            var fecha = fechas[i];

            if (diaCorteObjetivo > 28 &&
                fecha.Month == 2 &&
                fecha.Day == DateTime.DaysInMonth(fecha.Year, 2))
            {
                int diasFaltantes = diaCorteObjetivo - fecha.Day;
                // Forzamos el salto al mes siguiente (marzo) sumando los días que faltaron
                fecha = fecha.AddDays(diasFaltantes);
                fechas[i] = fecha; // Actualizamos la lista antes de evaluar si es hábil
            }

            var esHabil = await _calendarioLaboral.EsFechaHabilAsync(fecha);
            if (!esHabil)
            {
                var siguienteHabil = await _calendarioLaboral.SiguienteHabilAsync(fecha);
                fechas[i] = siguienteHabil;
            }
        }
        // ajusta la fecha a la final a la firma del contrato de la ultima amortización, si es que existe la fecha de firma del contrato
        if (amortization.FechaFirmaContrato.HasValue)
        {
            var ultAmorizacion = amortization.FechaFirmaContrato.Value.AddMonths(amortization.Plazo);
            //si esta fecha es no habil, se ajusta a la al anterior fecha hábil
            if(!await _calendarioLaboral.EsFechaHabilAsync(ultAmorizacion))
            {
                ultAmorizacion = await _calendarioLaboral.AnteriorHabilAsync(ultAmorizacion);
            }
            fechas[amortization.Plazo - 1] = ultAmorizacion;
        }


        return fechas;
    }
}
