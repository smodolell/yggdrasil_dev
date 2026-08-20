namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.Specifications;

public class TipoMovimientoSpec : Specification<CS_TipoMovimiento>
{
    public TipoMovimientoSpec(string? searchText = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p =>
                p.NomTipoMovimiento.Contains(searchText) ||
                p.Clave.Contains(searchText)
            );
        }
    }
}
