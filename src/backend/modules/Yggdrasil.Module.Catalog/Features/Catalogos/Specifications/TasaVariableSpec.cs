namespace Yggdrasil.Module.Catalog.Features.Catalogos.Specifications;

using Ardalis.Specification;

public class TasaVariableSpec : Specification<CAT_Tasa>
{
    public TasaVariableSpec(
        string? searchText = null,
        bool? activo = null)
    {

        Query.Where(p => p.EsVariable);


        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p => p.NomTasa.Contains(searchText));
        }

        if (activo.HasValue)
        {
            Query.Where(p => p.Activo == activo.Value);
        }
    }
}
