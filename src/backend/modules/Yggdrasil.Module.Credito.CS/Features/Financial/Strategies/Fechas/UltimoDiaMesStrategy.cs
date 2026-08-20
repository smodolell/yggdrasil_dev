using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Financial.Services;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Fechas;

public class UltimoDiaMesStrategy(ICalendarioLaboralService calendarioLaboral) : IFechasStrategies
{
    private readonly ICalendarioLaboralService _calendarioLaboral = calendarioLaboral;

    public async Task<List<DateTime>> GenerarCalendarioFechasAsync(AmortizationDto amortization)
    {
        
        var fechasBase = _calendarioLaboral.GenerarCalendarioFechasAsync(
            amortization.FecPrimeraRenta,
            amortization.UsaDias,
            amortization.ParamDias,
            amortization.ParamMes,
            amortization.Plazo
        );

        // Convertimos cada fecha generada exactamente al último día de su mes
        var fechasFinDeMes = fechasBase
            .Select(f => _calendarioLaboral.ObtenerUltimoDiaMes(f))
            .ToList();
        var result = fechasFinDeMes ?? new List<DateTime>();
        return await Task.FromResult(result);
    }
}
