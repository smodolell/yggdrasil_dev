using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Commands;

public record SyncSeccionPersonaCommand(List<SeccionPersonaDto> Model) : ICommand<Result>;

internal class SyncSeccionPersonaCommandHandler(
    IApplicationDbContext context,
    IMapper mapper
    ) : ICommandHandler<SyncSeccionPersonaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> HandleAsync(SyncSeccionPersonaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var seccionesRecibidas = message.Model;
            var seccionIdsRecibidos = seccionesRecibidas.Select(s => s.SeccionId).ToList();

            // 1. Obtener todas las secciones existentes
            var todasLasSecciones = await _context.FI_Seccion
                .ToListAsync(cancellationToken);

            // 2. Desactivar las secciones que no están en la lista recibida
            var seccionesADesactivar = todasLasSecciones
                .Where(s => !seccionIdsRecibidos.Contains(s.Id) && s.Activa)
                .ToList();

            foreach (var seccion in seccionesADesactivar)
            {
                seccion.Activa = false;
                _context.FI_Seccion.Update(seccion);
            }

            // 3. Actualizar o crear las secciones recibidas
            foreach (var seccionDto in seccionesRecibidas)
            {
                var oSeccion = await _context.FI_Seccion
                    .SingleOrDefaultAsync(r => r.Id == seccionDto.SeccionId, cancellationToken);

                if (oSeccion == null)
                {
                    // Crear nueva sección
                    oSeccion = new FI_Seccion
                    {
                        Id = seccionDto.SeccionId,
                        Activa = true
                    };
                    _context.FI_Seccion.Add(oSeccion);
                }
                else
                {
                    // Reactivar si estaba desactivada
                    oSeccion.Activa = true;
                }

                // Mapear los datos
                _mapper.Map(seccionDto, oSeccion);
            }

            // 4. Guardar todos los cambios
            await _context.SaveChangesAsync(cancellationToken);

            var mensaje = $"Secciones sincronizadas. " +
                          $"Actualizadas/Creadas: {seccionesRecibidas.Count}, " +
                          $"Desactivadas: {seccionesADesactivar.Count}";

            return Result.SuccessWithMessage(mensaje);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error al sincronizar secciones: {ex.Message}");
        }
    }
}
