namespace Yggdrasil.Module.Cobranza.Features.Catalogos.Specifications;

public class TipoPagoSpec : Specification<FI_TipoPago>
{
    public TipoPagoSpec(string? searchText)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p => p.NomTipoPago.Contains(searchText));
        }
    }
}