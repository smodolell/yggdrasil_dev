using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Financial.Services;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Fechas;

public class SiguienteDiaHabilStrategies(ICalendarioLaboralService calendarioLaboral) : IFechasStrategies
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
            var esHabil = await _calendarioLaboral.EsFechaHabilAsync(fecha);
            if (!esHabil)
            {
                var siguienteHabil = await _calendarioLaboral.SiguienteHabilAsync(fecha);
                fechas[i] = siguienteHabil; 
            }
        }

        return fechas;
    }
}
