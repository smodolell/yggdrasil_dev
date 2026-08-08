namespace Yggdrasil.Module.Credito.Features.Configuracion.Perfil.Specifications;


public class SeccionSpec : Specification<FI_Seccion>
{
    public SeccionSpec(string? searchText = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(r => r.NomSeccion.Contains(searchText));
        }
    }
}
