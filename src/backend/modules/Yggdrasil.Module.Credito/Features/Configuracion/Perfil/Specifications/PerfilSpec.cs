namespace Yggdrasil.Module.Credito.Features.Configuracion.Perfil.Specifications;

public class PerfilSpec : Specification<FI_Perfil>
{
    public PerfilSpec(string? searchText = null, bool? activo = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(r => r.NomPerfil.Contains(searchText));
        }

        if (activo.HasValue)
        {
            Query.Where(r => r.Activo == activo.Value);
        }
    }
}
