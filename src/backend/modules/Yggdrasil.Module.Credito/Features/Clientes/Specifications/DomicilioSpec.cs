namespace Yggdrasil.Module.Credito.Features.Clientes.Specifications;

public class DomicilioSpec : Specification<FI_Domicilio>
{
    public DomicilioSpec(int personaId, string? searchText = null)
    {
        // Siempre filtrar por PersonaId
        Query.Where(r => r.PersonaId == personaId);

        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(r =>
                r.Calle.Contains(searchText) ||
                r.Numero.Contains(searchText)
            );
        }
    }
}
