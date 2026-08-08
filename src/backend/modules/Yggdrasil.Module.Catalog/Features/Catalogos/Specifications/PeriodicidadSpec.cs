namespace Yggdrasil.Module.Catalog.Features.Catalogos.Specifications;

public class PeriodicidadSpec : Specification<CAT_Periodicidad>
{
    public PeriodicidadSpec(string? searchText = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p =>
                p.NomPeriodicidad.Contains(searchText) ||
                p.ClavePeriodicidad.Contains(searchText)
            );
        }
    }
}
