namespace Yggdrasil.Module.Catalog.Features.Catalogos.Specifications;

public class BancoSpec : Specification<CAT_Banco>
{
    public BancoSpec(string? searchText = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p =>
                p.NomBanco.Contains(searchText) ||
                p.CodigoBCRA.Contains(searchText) ||
                p.CBUPrefix.Contains(searchText)
            );
        }
    }
}
