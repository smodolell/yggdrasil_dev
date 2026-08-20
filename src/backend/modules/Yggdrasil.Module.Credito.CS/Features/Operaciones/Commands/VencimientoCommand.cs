namespace Yggdrasil.Module.Credito.CS.Features.Operaciones.Commands;

public record VencimientoCommand(VencimientoDto Model) : ICommand<Result<VencimientoResultDto>>;

internal class VencimientoCommandHandler(IUnitOfWork unitOfWork, IApplicationDbContext context) : ICommandHandler<VencimientoCommand, Result<VencimientoResultDto>>
{
    public async Task<Result<VencimientoResultDto>> HandleAsync(VencimientoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        model.FechaInicial = model.FechaInicial ?? DateTime.Now;
        model.FechaFinal = model.FechaFinal ?? DateTime.Now;

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        var oTablaAmortizas = await context.CS_TablaAmortiza
            .Include(i => i.CS_Credito)
            .Include(i => i.CS_TipoMovimiento)
            .Where(r => !r.Procesado &&
                    r.FechaVencimiento >= model.FechaInicial &&
                    r.FechaVencimiento <= model.FechaFinal &&
                    (model.CreditoId == null || r.CreditoId == model.CreditoId))
            .ToListAsync();

        if (!oTablaAmortizas.Any())
        {
            return Result.CriticalError("No hay movimientos para procesar.");
        }

        var oMovimientos = oTablaAmortizas.Select(s => new CS_Movimiento
        {
            TipoMovimientoId = s.TipoMovimientoId,
            CreditoId = s.CreditoId,
            DescMovimiento = $"({s.NoPago}/{s.CS_Credito.Plazo}) {s.CS_TipoMovimiento.NomTipoMovimiento}",
            FechaRegistro = DateTime.UtcNow,
            FechaVencimiento = s.FechaVencimiento,
            Capital = s.Capital,
            Interes = s.Interes,
            Iva = s.Iva,
            Total = s.Total,
            SaldoCapital = s.Capital,
            SaldoInteres = s.Interes,
            SaldoIva = s.Iva,
            SaldoTotal = s.Total,
            NoPago = s.NoPago
        }).ToList();

        await context.CS_Movimiento.AddRangeAsync(oMovimientos);

        await context.SaveChangesAsync();

        var idsProcesados = oTablaAmortizas.Select(m => m.Id).Distinct().ToList();

        await context.FI_TablaAmortiza
          .Where(fta => idsProcesados.Contains(fta.Id) && !fta.Procesado)
          .ExecuteUpdateAsync(u => u.SetProperty(b => b.Procesado, true));

        await unitOfWork.CommitTransactionAsync();

        return Result.Success(new VencimientoResultDto(false, "Proceso terminado", idsProcesados.Count));
    }
}
public class VencimientoDto
{
    public int? CreditoId { get; set; }
    public DateTime? FechaInicial { get; set; }
    public DateTime? FechaFinal { get; set; }
}

public record VencimientoResultDto(bool HasError, string MessageProcess, int Cantidad);
