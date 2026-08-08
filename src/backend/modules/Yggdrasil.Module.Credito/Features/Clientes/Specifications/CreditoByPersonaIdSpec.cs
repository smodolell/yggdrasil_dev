namespace Yggdrasil.Module.Credito.Features.Clientes.Specifications;

public class CreditoByPersonaIdSpec : Specification<FI_Credito>
{
    public CreditoByPersonaIdSpec(
        int personaId,
        int? productoId = null,
        string? searchText = null,
        int? estatusCreditoId = null)
    {
        // Siempre filtrar por PersonaId
        Query.Where(p => p.PersonaId == personaId);

        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(r => r.ClaveCredito.Contains(searchText));
        }

        if (productoId.HasValue)
        {
            Query.Where(r => r.ProductoId == productoId.Value);
        }

        if (estatusCreditoId.HasValue)
        {
            Query.Where(r => r.EstatusCreditoId == estatusCreditoId.Value);
        }
    }
}
