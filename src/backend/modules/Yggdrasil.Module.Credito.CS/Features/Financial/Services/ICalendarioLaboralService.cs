namespace Yggdrasil.Module.Credito.CS.Features.Financial.Services;

public interface ICalendarioLaboralService
{
    Task<bool> EsFechaHabilAsync(DateTime fecha);
    Task<DateTime> SiguienteHabilAsync(DateTime fecha);
    Task<DateTime> AnteriorHabilAsync(DateTime fecha);
    List<DateTime> GenerarCalendarioFechasAsync(
        DateTime fechaPrimeraRenta,
        bool usaDias,
        int paramDias,
        int paramMeses,
        int plazo
    );

    DateTime ObtenerUltimoDiaMes(DateTime fecha);


    DateTime MoverADiaEspecifico(DateTime fecha, int dia);
}
