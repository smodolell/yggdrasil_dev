namespace Yggdrasil.Module.Credito.CS.Features.Catalogos.Specifications;

public class TipoPagoSpec : Specification<CS_TipoPago>
{
    public TipoPagoSpec(string? searchText = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p => p.NomTipoPago.Contains(searchText));
        }
    }
}
