namespace Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.Commands;

public record ChangeActivoTipoMovimientoCommand(int TipoMovimientoId, bool Activo) : ICommand<Result>;

internal class ChangeActivoTipoMovimientoCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<ChangeActivoTipoMovimientoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(
        ChangeActivoTipoMovimientoCommand message,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var oTipoMovimiento = await _context.FI_TipoMovimiento
                .SingleOrDefaultAsync(r => r.Id == message.TipoMovimientoId, cancellationToken);

            if (oTipoMovimiento == null)
                return Result.Error($"[NO_EXISTE][{nameof(FI_TipoMovimiento)}]");

            oTipoMovimiento.Activo = message.Activo;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.SuccessWithMessage(message.Activo
                ? "TIPO MOVIMIENTO ACTIVADO"
                : "TIPO MOVIMIENTO DESACTIVADO");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}