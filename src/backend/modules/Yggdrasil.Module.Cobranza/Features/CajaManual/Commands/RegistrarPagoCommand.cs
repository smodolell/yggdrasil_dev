using Yggdrasil.Common.Attributes;
using Yggdrasil.Module.Cobranza.Features.CajaManual.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.CajaManual.Commands;

[Auditable(AuditEvents.RegistrarPago)]
public record RegistrarPagoCommand(PagoDto Model) : ICommand<Result<PagoResultDto>>;

internal class RegistrarPagoCommandHandler(
    IApplicationDbContext context,
    IUnitOfWork unitOfWork,
    IValidator<PagoDto> validator
) : ICommandHandler<RegistrarPagoCommand, Result<PagoResultDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<PagoDto> _validator = validator;

    public async Task<Result<PagoResultDto>> HandleAsync(RegistrarPagoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;

        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var creditoExiste = await _context.FI_Credito
            .AnyAsync(c => c.Id == model.CreditoId, cancellationToken);

        if (!creditoExiste)
            return Result.NotFound($"[NO_EXISTE][{nameof(FI_Credito)}]");

        var movimientos = await _context.FI_Movimiento
            .Where(m => m.CreditoId == model.CreditoId
               && model.Movimientos.Contains(m.Id)
               && m.SaldoTotal > 0)
            .OrderBy(m => m.FechaVencimiento)
            .ThenBy(m => m.NoPago)
            .ToListAsync(cancellationToken);

        if (!movimientos.Any())
            return Result.Error("No hay movimientos pendientes de pago para este crédito.");

        // Validar que todos los movimientos seleccionados existan
        var idsEncontrados = movimientos.Select(m => m.Id).ToHashSet();
        var idsNoEncontrados = model.Movimientos.Where(id => !idsEncontrados.Contains(id)).ToList();

        if (idsNoEncontrados.Any())
            return Result.Error($"Los siguientes movimientos no existen o ya están pagados: {string.Join(", ", idsNoEncontrados)}");

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var pago = new FI_Pago
            {
                TipoPagoId = model.TipoPagoId,
                FechaPago = model.FechaPago,
                FechaRegistro = DateTime.UtcNow,
                FechaModificacion = DateTime.UtcNow,
                Monto = model.Monto,
                SaldoFavor = 0,
                Cancelado = false,
                Suspenso = false,
                Activo = true,
                CorrelationId = Guid.NewGuid()
            };
            await _context.FI_Pago.AddAsync(pago, cancellationToken);

            decimal montoRestante = model.Monto;
            int movimientosLiquidados = 0;
            var pagoMovimientos = new List<FI_PagoMovimiento>();

            foreach (var mov in movimientos)
            {
                if (montoRestante <= 0) break;

                decimal aplicar = Math.Min(montoRestante, mov.SaldoTotal);
                decimal proporcion = aplicar / mov.SaldoTotal;

                decimal capitalPagado = Math.Round(mov.SaldoCapital * proporcion, 2);
                decimal interesPagado = Math.Round(mov.SaldoInteres * proporcion, 2);
                decimal ivaPagado = Math.Round(mov.SaldoIva * proporcion, 2);
                decimal totalPagado = capitalPagado + interesPagado + ivaPagado;

                pagoMovimientos.Add(new FI_PagoMovimiento
                {
                    FI_Pago = pago,
                    MovimientoId = mov.Id,
                    TotalPagado = totalPagado,
                    CapitalPagado = capitalPagado,
                    InteresPagado = interesPagado,
                    IvaPagado = ivaPagado,
                    FechaPago = model.FechaPago,
                    Cancelado = false,
                    Activo = true
                });

                mov.SaldoCapital -= capitalPagado;
                mov.SaldoInteres -= interesPagado;
                mov.SaldoIva -= ivaPagado;
                mov.SaldoTotal -= totalPagado;

                if (mov.SaldoTotal <= 0)
                    movimientosLiquidados++;

                montoRestante -= totalPagado;
            }

            pago.SaldoFavor = montoRestante > 0 ? montoRestante : 0;

            await _context.FI_PagoMovimiento.AddRangeAsync(pagoMovimientos, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return Result.Success(new PagoResultDto
            {
                PagoId = pago.Id,
                MontoAplicado = model.Monto - pago.SaldoFavor,
                SaldoFavor = pago.SaldoFavor,
                MovimientosLiquidados = movimientosLiquidados,
                Mensaje = $"Pago registrado. Movimientos liquidados: {movimientosLiquidados}."
            });
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Error($"Error crítico al registrar el pago: {ex.Message}");
        }
    }
}
