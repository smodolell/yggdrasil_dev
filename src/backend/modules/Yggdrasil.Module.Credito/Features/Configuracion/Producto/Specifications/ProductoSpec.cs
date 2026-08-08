namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Specifications;

public class ProductoSpec : Specification<FI_Producto>
{
    public ProductoSpec(string? searchText = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p => p.NomProducto.Contains(searchText) || p.ClaveProducto.Contains(searchText));
        }
    }
}
