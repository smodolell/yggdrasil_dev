using System.Reflection;

namespace Yggdrasil.Module.Credito.UI.Services.Sync;


public interface ISeccionPersonaSyncService
{
    Task<bool> SyncAllSectionsAsync();
    Task<bool> SyncSectionsFromAssemblyAsync(Assembly assembly);
}
