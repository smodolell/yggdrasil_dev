namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Specifications;

public class CargoInicialSpec : Specification<FI_Cargo>
{
    public CargoInicialSpec(int productoId)
    {
        Query.Where(p => p.ProductoId == productoId);
        Query.Where(p => p.EsCargoInicial);
    }
}
