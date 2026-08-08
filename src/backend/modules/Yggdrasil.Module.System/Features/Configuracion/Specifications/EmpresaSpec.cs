namespace Yggdrasil.Module.System.Features.Configuracion.Specifications;

public class EmpresaSpec : Specification<CAT_Empresa>
{
    public EmpresaSpec(string? searchText = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p => p.NomEmpresa.Contains(searchText));
        }
    }
}
