namespace Yggdrasil.Module.Catalog.Features.Catalogos.Specifications;

public class MonedaSpec : Specification<CAT_Moneda>
{
    public MonedaSpec(string? searchText = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p =>
                p.NomMoneda.Contains(searchText) ||
                p.ClaveMoneda.Contains(searchText)
            );
        }
    }
}
