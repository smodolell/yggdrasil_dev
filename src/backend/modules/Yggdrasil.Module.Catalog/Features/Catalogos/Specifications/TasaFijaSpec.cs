namespace Yggdrasil.Module.Catalog.Features.Catalogos.Specifications;

using Ardalis.Specification;

public class TasaFijaSpec : Specification<CAT_Tasa>
{
    public TasaFijaSpec(
        decimal? valueMin = null,
        decimal? valueMax = null,
        string? searchText = null,
        bool? activo = null)
    {

        Query.Where(p => !p.EsVariable);

        if (valueMin.HasValue)
        {
            Query.Where(p => p.ValorTasa >= valueMin.Value);
        }

        if (valueMax.HasValue)
        {
            Query.Where(p => p.ValorTasa <= valueMax.Value);
        }

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
