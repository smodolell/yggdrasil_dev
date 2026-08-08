namespace Yggdrasil.Module.Credito.Features.Clientes.Specifications;

public class PersonaSpec : Specification<FI_Persona>
{
    public PersonaSpec(
        string? searchText = null,
        int? perfilId = null,
        int? generoId = null,
        int? edoCivilId = null,
        DateTime? fechaAltaClienteStart = null,
        DateTime? fechaAltaClienteEnd = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            var terms = searchText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Query.Where(p =>
                terms.Any(temp =>
                    p.PrimerNombre.Contains(temp) ||
                    p.SegundoNombre.Contains(temp) ||
                    p.ApellidoPaterno.Contains(temp) ||
                    p.ApellidoMaterno.Contains(temp) ||
                    p.RFC.Contains(temp) ||
                    p.CURP.Contains(temp)
                )
            );
        }

        if (perfilId.HasValue)
        {
            Query.Where(p => p.PerfilId == perfilId.Value);
        }

        if (generoId.HasValue)
        {
            Query.Where(p => p.GeneroId == generoId.Value);
        }

        if (edoCivilId.HasValue)
        {
            Query.Where(p => p.EdoCivilId == edoCivilId.Value);
        }


        if (fechaAltaClienteStart.HasValue)
        {
            Query.Where(p => p.FechaAltaCliente.Date >= fechaAltaClienteStart.Value.Date);
        }

        if (fechaAltaClienteEnd.HasValue)
        {
            Query.Where(p => p.FechaAltaCliente.Date <= fechaAltaClienteEnd.Value.Date);
        }
        Query.Include(i => i.FI_Perfil);
    }
}
