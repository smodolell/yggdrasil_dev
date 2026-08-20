namespace Yggdrasil.Module.Credito.CS.Features.Financial.Services;

public class CalendarioLaboralService(IApplicationDbContext context) : ICalendarioLaboralService
{
    private readonly IApplicationDbContext _context = context;

    public async Task<DateTime> AnteriorHabilAsync(DateTime fecha)
    {
        var calendario = await _context.CAT_CalendarioLaboral
            .Where(c => c.Fecha < fecha.Date && c.EsHabil)
            .OrderByDescending(c => c.Fecha)
            .FirstOrDefaultAsync();

        if (calendario == null)
        {
            throw new InvalidOperationException($"No se encontró un día hábil anterior para la fecha {fecha:yyyy-MM-dd}");
        }
        return calendario.Fecha;
    }
    public async Task<bool> EsFechaHabilAsync(DateTime fecha)
    {
        var calendario = await _context.CAT_CalendarioLaboral
            .FirstOrDefaultAsync(c => c.Fecha.Date == fecha.Date);
        if (calendario == null)
        {
            throw new InvalidOperationException($"No se encontró información de calendario para la fecha {fecha:yyyy-MM-dd}");
        }
        return calendario.EsHabil;
    }




    public List<DateTime> GenerarCalendarioFechasAsync(
        DateTime fechaPrimeraRenta,
        bool usaDias,
        int paramDias, int paramMeses,
        int plazo
        )
    {
        var fechas = new List<DateTime>();
        for (int i = 0; i < plazo; i++)
        {
            if (usaDias)
            {
                fechas.Add(fechaPrimeraRenta.AddDays(paramDias * i));
            }
            else
            {
                // Lógica de meses con ajuste de fin de mes
                var fecha = fechaPrimeraRenta.AddMonths(i * paramMeses);
                fechas.Add(fecha);
            }
        }
        return fechas;
    }

    public async Task<DateTime> SiguienteHabilAsync(DateTime fecha)
    {
        var calendario = await _context.CAT_CalendarioLaboral
            .Where(c => c.Fecha.Date > fecha.Date && c.EsHabil)
            .OrderBy(c => c.Fecha)
            .FirstOrDefaultAsync();

        if (calendario == null)
        {
            throw new InvalidOperationException($"No se encontró un día hábil posterior para la fecha {fecha:yyyy-MM-dd}");
        }
        return calendario.Fecha;
    }

    /// <summary>
    /// Retorna el último día del mes para la fecha especificada.
    /// </summary>
    public DateTime ObtenerUltimoDiaMes(DateTime fecha)
    {
        var diasEnMes = DateTime.DaysInMonth(fecha.Year, fecha.Month);
        return new DateTime(fecha.Year, fecha.Month, diasEnMes, fecha.Hour, fecha.Minute, fecha.Second);
    }

    public DateTime MoverADiaEspecifico(DateTime fecha, int dia)
    {
        // Validar que el día sea válido para el mes
        var ultimoDia = DateTime.DaysInMonth(fecha.Year, fecha.Month);

        if (dia < 1 || dia > ultimoDia)
            throw new ArgumentException($"El día {dia} no es válido para {fecha.Month}/{fecha.Year}. Debe estar entre 1 y {ultimoDia}");

        return new DateTime(fecha.Year, fecha.Month, dia, fecha.Hour, fecha.Minute, fecha.Second, fecha.Kind);
    }
}
