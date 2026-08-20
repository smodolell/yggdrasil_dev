namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.Specifications;

public class TipoCreditoSpec : Specification<CS_TipoCredito>
{
    public TipoCreditoSpec(string? searchText = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p =>
                p.NomTipoCredito.Contains(searchText) ||
                p.ClaveTipoCredito.Contains(searchText)
            );
        }
    }
}
