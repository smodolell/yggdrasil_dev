using Yggdrasil.Common.Extensions;
using Yggdrasil.Module.Credito.CS.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.Commands;

public record SyncMetodoArmotizacionCommand : ICommand<Result>;

internal class SyncMetodoArmotizacionCommandHandler(
    IApplicationDbContext context,
    IAmortizationStrategyFactory amortizationStrategyFactory)
    : ICommandHandler<SyncMetodoArmotizacionCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IAmortizationStrategyFactory _amortizationStrategyFactory = amortizationStrategyFactory;

    public async Task<Result> HandleAsync(
        SyncMetodoArmotizacionCommand message,
        CancellationToken cancellationToken = default)
    {
        // Obtener métodos disponibles en DI (fuente de verdad)
        var availableMethods = _amortizationStrategyFactory.GetAvailableMethods();
        var availableIds = availableMethods.Select(m => (int)m).ToHashSet();

        // 1. Preparar métodos para agregar/actualizar
        var methodsToUpsert = availableMethods
            .Select(m => new CS_MetodoArmotizacion
            {
                Id = (int)m,
                NomMetodoArmotizacion = m.GetDescription(),
                Activo = true
            })
            .ToList();

        // 2. Obtener todos los registros existentes en BD
        var existingRecords = await _context.CS_MetodoArmotizacion
            .ToListAsync(cancellationToken);

        var existingIds = existingRecords.Select(r => r.Id).ToHashSet();

        // 3. Agregar nuevos registros
        var toAdd = methodsToUpsert.Where(m => !existingIds.Contains(m.Id)).ToList();
        if (toAdd.Any())
        {
            await _context.CS_MetodoArmotizacion.AddRangeAsync(toAdd, cancellationToken);
        }

        // 4. Actualizar registros existentes (por si cambió la descripción)
        var toUpdate = existingRecords
            .Where(r => availableIds.Contains(r.Id))
            .ToList();

        foreach (var record in toUpdate)
        {
            var newDescription = availableMethods
                .First(m => (int)m == record.Id)
                .GetDescription();

            if (record.NomMetodoArmotizacion != newDescription)
            {
                record.NomMetodoArmotizacion = newDescription;
            }
            record.Activo = true;
        }

        // 5. DESACTIVAR o ELIMINAR registros que existen en BD pero NO en DI
        var toDisable = existingRecords
            .Where(r => !availableIds.Contains(r.Id))
            .ToList();

        if (toDisable.Any())
        {
            // Opción A: Desactivación lógica (recomendada)
            foreach (var record in toDisable)
            {
                // Asumiendo que tienes un campo Activo (bool)
                // Si no existe, agrégalo a tu entidad
                record.Activo = false;
            }

            // Opción B: Eliminación física (si prefieres borrar)
            // _context.BF_TipoTablaAmortiza.RemoveRange(toDisable);
        }

        // 6. Guardar todos los cambios en una sola transacción
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}