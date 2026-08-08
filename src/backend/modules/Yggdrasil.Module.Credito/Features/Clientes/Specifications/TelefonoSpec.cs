namespace Yggdrasil.Module.Credito.Features.Clientes.Specifications;

public class TelefonoSpec : Specification<FI_Telefono>
{
    public TelefonoSpec(int personaId, string? searchText = null, int? tipoTelefonoId = null)
    {
        // Siempre filtrar por PersonaId
        Query.Where(r => r.PersonaId == personaId);

        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(r => r.Numero.Contains(searchText));
        }

        if (tipoTelefonoId.HasValue)
        {
            Query.Where(r => r.TipoTelefonoId == tipoTelefonoId.Value);
        }
    }
}
