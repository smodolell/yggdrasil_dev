using Microsoft.Extensions.Logging;
using System.Reflection;
using Yggdrasil.ApiClient;
using Yggdrasil.ApiClient.Contracts;
using Yggdrasil.Module.Credito.UI.Helpers;


namespace Yggdrasil.Module.Credito.UI.Services.Sync;

public class SeccionPersonaSyncService : ISeccionPersonaSyncService
{
    private readonly ICreditoClientesApi _clientesApi;
    private readonly ILogger<SeccionPersonaSyncService> _logger;

    public SeccionPersonaSyncService(ICreditoClientesApi clientesApi, ILogger<SeccionPersonaSyncService> logger)
    {
        _clientesApi = clientesApi;
        _logger = logger;
    }

    public async Task<bool> SyncAllSectionsAsync()
    {
        try
        {
            var todasLasSecciones = new List<SeccionPersonaAssemblyDto>();

            // Obtener el assembly actual
            var currentAssembly = Assembly.GetExecutingAssembly();
            var secciones = SeccionPersonaHelper.GetListSeccionByAssembly(currentAssembly);
            todasLasSecciones.AddRange(secciones);

            // Buscar en assemblies referenciados que contengan CreditFlow
            var referencedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName?.Contains("Yggdrasil.CreditFlow") == true
                         && a != currentAssembly);

            foreach (var assembly in referencedAssemblies)
            {
                var seccionesRef = SeccionPersonaHelper.GetListSeccionByAssembly(assembly);
                todasLasSecciones.AddRange(seccionesRef);
            }

            return await SyncSectionsAsync(todasLasSecciones);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener secciones");
            return false;
        }
    }

    public async Task<bool> SyncSectionsFromAssemblyAsync(Assembly assembly)
    {
        try
        {
            var secciones = SeccionPersonaHelper.GetListSeccionByAssembly(assembly);
            return await SyncSectionsAsync(secciones);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al sincronizar secciones del assembly {Assembly}", assembly.FullName);
            return false;
        }
    }

    private async Task<bool> SyncSectionsAsync(List<SeccionPersonaAssemblyDto> secciones)
    {
        try
        {
            var seccionesDto = secciones.Select(s => new SeccionPersonaDto
            {
                SeccionId = s.SeccionId,
                NomSeccion = s.NomSeccion,
                IsCreate = s.IsCreate,
                IsEdit = s.IsEdit,
                IsExtension = s.IsExtension
            }).ToList();

            var result = await _clientesApi.SyncSeccionPersonaAsync(seccionesDto);

            if (result.Success)
            {
                _logger.LogInformation("Secciones sincronizadas: {Message}", result.Message);
                return true;
            }
            else
            {
                _logger.LogError("Error: {Errors}", string.Join(", ", result.Errors ?? new List<string>()));
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error durante la sincronización");
            return false;
        }
    }
}