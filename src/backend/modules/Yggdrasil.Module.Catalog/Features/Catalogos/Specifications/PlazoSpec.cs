namespace Yggdrasil.Module.Catalog.Features.Catalogos.Specifications;

public class PlazoSpec : Specification<CAT_Plazo>
{
    public PlazoSpec(int? valorPlazo = null, bool? activo = null)
    {
        if (valorPlazo.HasValue)
        {
            Query.Where(p => p.ValorPlazo == valorPlazo.Value);
        }

        if (activo.HasValue)
        {
            Query.Where(p => p.Activo == activo.Value);
        }
    }
}
