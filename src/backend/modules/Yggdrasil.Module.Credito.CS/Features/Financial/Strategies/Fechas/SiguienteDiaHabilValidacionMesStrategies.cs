using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Financial.Services;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Fechas;

public class SiguienteDiaHabilValidacionMesStrategies(ICalendarioLaboralService calendarioLaboral) : IFechasStrategies
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

        for (int i = 0; i < fechas.Count; i++)
        {
            var fecha = fechas[i];
            if (!await _calendarioLaboral.EsFechaHabilAsync(fecha))
            {
                var siguienteHabil = await _calendarioLaboral.SiguienteHabilAsync(fecha);

                if (siguienteHabil.Month != fecha.Month)
                {
                    // Si saltó al mes siguiente, retrocedemos al último hábil del mes origen
                    fechas[i] = await _calendarioLaboral.AnteriorHabilAsync(fecha);
                }
                else
                {
                    fechas[i] = siguienteHabil;
                }
            }
        }

        return fechas;
    }
}
