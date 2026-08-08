namespace Yggdrasil.Module.Auth.Features.Rol.Specifications;

public class RolSpec : Specification<SYS_Rol>
{
    public RolSpec(string? searchText = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p =>
                (p.Name != null && p.Name.Contains(searchText)) ||
                (p.Descripcion != null && p.Descripcion.Contains(searchText))
            );
        }
    }
}
