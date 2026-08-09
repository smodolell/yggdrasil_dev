using Yggdrasil.Common.Attributes;
using Yggdrasil.Module.Credito.Features.Operaciones.ActivarCredito.DTOs;

namespace Yggdrasil.Module.Credito.Features.Operaciones.ActivarCredito.Commands;

public record ActivarCreditoCommand(int CreditoId) : ICommand<Result<ActivarCreditoResultDto>>;

[Auditable(AuditEvents.ActivarCredito)]
internal class ActivarCreditoCommandHandler(
    IApplicationDbContext context,
    IUnitOfWork unitOfWork
) : ICommandHandler<ActivarCreditoCommand, Result<ActivarCreditoResultDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<ActivarCreditoResultDto>> HandleAsync(ActivarCreditoCommand message, CancellationToken cancellationToken = default)
    {
        var creditoId = message.CreditoId;

        try
        {
            // 1. Verificar que el crédito existe
            var credito = await _context.FI_Credito
                .FirstOrDefaultAsync(c => c.Id == creditoId, cancellationToken);

            if (credito == null)
                return Result.NotFound($"No se encontró el crédito con ID {creditoId}");

            // 2. Verificar que el crédito no esté ya activo
            if (credito.EstatusCreditoId == 2)
                return Result.Invalid(new ValidationError("El crédito ya se encuentra activo"));

            // Iniciar transacción
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            // 3. Actualizar el crédito a activo (EstatusCreditoId = 2)
            await _context.FI_Credito
                .Where(c => c.Id == creditoId)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(c => c.EstatusCreditoId, 2)
                    .SetProperty(c => c.FechaActivacion, DateTime.Now),
                    cancellationToken);

            // 4. Obtener la primera amortización (NoPago = 1)
            var primeraAmortizacion = await _context.FI_TablaAmortiza
                .Include(fta => fta.FI_Credito)
                .Include(fta => fta.FI_TipoMovimiento)
                .Where(fta => fta.CreditoId == creditoId && fta.NoPago == 1)
                .FirstOrDefaultAsync(cancellationToken);

            if (primeraAmortizacion == null)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result.Invalid(new ValidationError("No se encontró la tabla de amortización para este crédito"));
            }

            // 5. Insertar el movimiento correspondiente a la primera amortización
            var movimiento = new FI_Movimiento
            {
                TipoMovimientoId = primeraAmortizacion.TipoMovimientoId,
                CreditoId = creditoId,
                DescMovimiento = $"({primeraAmortizacion.NoPago}/{primeraAmortizacion.FI_Credito.Plazo}) {primeraAmortizacion.FI_TipoMovimiento.NomTipoMovimiento}",
                FechaRegistro = DateTime.Now,
                FechaVencimiento = primeraAmortizacion.FechaVencimiento,
                Capital = primeraAmortizacion.Capital,
                Interes = primeraAmortizacion.Interes,
                Iva = primeraAmortizacion.Iva,
                Total = primeraAmortizacion.Total,
                SaldoCapital = primeraAmortizacion.Capital,
                SaldoInteres = primeraAmortizacion.Interes,
                SaldoIva = primeraAmortizacion.Iva,
                SaldoTotal = primeraAmortizacion.Total,
                NoPago = primeraAmortizacion.NoPago
            };

            await _context.FI_Movimiento.AddAsync(movimiento, cancellationToken);

            // 6. Marcar la primera amortización como procesada
            await _context.FI_TablaAmortiza
                .Where(fta => fta.CreditoId == creditoId && fta.NoPago == 1)
                .ExecuteUpdateAsync(u => u.SetProperty(fta => fta.Procesado, true),
                    cancellationToken);

            // 7. Guardar cambios y confirmar transacción
            await _context.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            // 8. Retornar resultado exitoso
            return Result.Success(new ActivarCreditoResultDto
            {
                HasError = false,
                MessageProcess = "PROCESO DE ACTIVACION CORRECTAMENTE",
                CreditoId = creditoId,
                ClaveCredito = credito.ClaveCredito
            });
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Error($"Error crítico al activar el crédito: {ex.Message}");
        }
    }
}