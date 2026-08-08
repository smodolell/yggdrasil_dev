namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Specifications;

public class ConceptoFinanciadoSpec : Specification<FI_Cargo>
{
    public ConceptoFinanciadoSpec(int productoId)
    {
        Query.Where(p => p.ProductoId == productoId);
        Query.Where(p => p.EsConceptoFinanciado);
    }
}
