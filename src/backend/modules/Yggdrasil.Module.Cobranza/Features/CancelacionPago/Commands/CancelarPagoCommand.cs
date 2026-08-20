using Yggdrasil.Common.Attributes;
using Yggdrasil.Module.Cobranza.Features.CancelacionPago.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.CancelacionPago.Commands;

[Auditable(AuditEvents.CancelarPago)]
public record CancelarPagoCommand(CancelarPagoDto Model) : ICommand<Result>;

internal class CancelarPagoCommandHandler(
    IApplicationDbContext context,
    IUnitOfWork unitOfWork,
    IValidator<CancelarPagoDto> validator
) : ICommandHandler<CancelarPagoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<CancelarPagoDto> _validator = validator;

    public async Task<Result> HandleAsync(CancelarPagoCommand message, CancellationToken cancellationToken = default)
    {
        var dto = message.Model;

        var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            foreach (var item in dto.Pagos)
            {
                var oPagoMovimiento = await _context.FI_PagoMovimiento
                    .Include(i => i.FI_Movimiento)
                    .Include(i => i.FI_Pago)
                        .ThenInclude(p => p.FI_PagoMovimiento)
                    .SingleOrDefaultAsync(
                        r => r.PagoId == item.PagoId && r.MovimientoId == item.MovimientoId,
                        cancellationToken);

                if (oPagoMovimiento == null) continue;

                var oMovimiento = oPagoMovimiento.FI_Movimiento;
                var oPago = oPagoMovimiento.FI_Pago;

                // Restaurar saldos del movimiento
                oMovimiento.SaldoCapital += oPagoMovimiento.CapitalPagado;
                oMovimiento.SaldoInteres += oPagoMovimiento.InteresPagado;
                oMovimiento.SaldoIva += oPagoMovimiento.IvaPagado;
                oMovimiento.SaldoTotal += oPagoMovimiento.TotalPagado;

                oPago.SaldoFavor -= oPagoMovimiento.TotalPagado;

                oPagoMovimiento.CapitalPagado = 0;
                oPagoMovimiento.InteresPagado = 0;
                oPagoMovimiento.IvaPagado = 0;
                oPagoMovimiento.TotalPagado = 0;
                oPagoMovimiento.Cancelado = true;
                oPagoMovimiento.MotivoCancelacion = dto.MotivoCancelacion;
                oPagoMovimiento.Activo = false;

                // Si no quedan PagoMovimientos activos (colección rastreada, refleja cambios ya hechos en este loop), cancelar el pago completo
                if (oPago.FI_PagoMovimiento.All(pm => !pm.Activo))
                {
                    oPago.Activo = false;
                    oPago.Cancelado = true;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Error($"Error crítico al cancelar el pago: {ex.Message}");
        }
    }
}
