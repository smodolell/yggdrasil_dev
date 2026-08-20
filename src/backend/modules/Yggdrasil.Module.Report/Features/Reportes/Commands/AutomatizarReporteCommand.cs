using Yggdrasil.Module.Report.Features.Reportes.DTOs;

namespace Yggdrasil.Module.Report.Features.Reportes.Commands;

public class AutomatizarReporteCommand : ICommand<Result>
{
    public required ReporteExecuteDto Model { get; set; }
}

public class AutomatizarReporteCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<AutomatizarReporteCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(AutomatizarReporteCommand message, CancellationToken cancellationToken = default)
    {
        // TODO: Implementar automatización de reportes.
        // Requiere agregar a IApplicationDbContext y al dominio:
        //   - DbSet<RSP_ReporteAutomatico> RSP_ReporteAutomatico
        //   - DbSet<RSP_ParametroValue> RSP_ParametroValue
        // Una vez agregadas, recuperar lógica original de AutomatizarReporte
        // en Migrate/ReporteStoredProcedureService.cs

        await Task.CompletedTask;
        return Result.Error("Funcionalidad pendiente: se requieren entidades RSP_ReporteAutomatico y RSP_ParametroValue.");
    }
}
