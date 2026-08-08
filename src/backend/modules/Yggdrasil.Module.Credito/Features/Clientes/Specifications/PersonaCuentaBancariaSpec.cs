namespace Yggdrasil.Module.Credito.Features.Clientes.Specifications;

public class PersonaCuentaBancariaSpec : Specification<FI_PersonaCuentaBancaria>
{
    public PersonaCuentaBancariaSpec(string? searchText = null, int? bancoId = null, int? monedaId = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p =>
                p.NroCuentaBancaria.Contains(searchText) ||
                p.CBU.Contains(searchText) ||
                p.AliasCBU.Contains(searchText)
            );
        }

        if (bancoId.HasValue)
        {
            Query.Where(p => p.BancoId == bancoId.Value);
        }

        if (monedaId.HasValue)
        {
            Query.Where(p => p.MonedaId == monedaId.Value);
        }
    }
}
