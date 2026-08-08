namespace Yggdrasil.Module.Auth.Features.Usuario.Specifications;

public class UsuarioSpec : Specification<SYS_Usuario>
{
    public UsuarioSpec(string? searchText)
    {
        // Siempre excluir al WEBMASTER
        Query.Where(p => p.NormalizedUserName != "WEBMASTER");

        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p =>
                (p.UserName != null && p.UserName.Contains(searchText)) ||
                p.NombreCompleto.Contains(searchText) ||
                (p.Email != null && p.Email.Contains(searchText)) ||
                (p.Telefono != null && p.Telefono.Contains(searchText))
            );
        }
    }
}
