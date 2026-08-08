namespace Yggdrasil.Module.Catalog.Features.Catalogos.Specifications;


public class TasaIvaSpec : Specification<CAT_TasaIva>
{
    public TasaIvaSpec(string? searchText = null, bool? activo = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p => p.NomTasaIva.Contains(searchText));
        }

        if (activo.HasValue)
        {
            Query.Where(p => p.Activo == activo.Value);
        }
    }
}
