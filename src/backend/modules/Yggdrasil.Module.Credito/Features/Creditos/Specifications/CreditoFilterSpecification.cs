namespace Yggdrasil.Module.Credito.Features.Creditos.Specifications;
public class CreditoFilterSpecification : Specification<FI_Credito>
{
    public CreditoFilterSpecification(
        string? searchText = null,
        int? productoId = null,
        int? estatusCreditoId = null,
        DateTime? fechaActivacionStart = null,
        DateTime? fechaActivacionEnd = null)
    {
        Query.Include(c => c.FI_Persona)
             .Include(c => c.FI_Producto)
             .Include(c => c.FI_EstatusCredito);

        ApplySearchTextFilter(searchText);
        ApplyEstatusFilter(estatusCreditoId);
        ApplyProductoFilter(productoId);
        ApplyFechaActivacionFilter(fechaActivacionStart, fechaActivacionEnd);
    }

    private void ApplySearchTextFilter(string? searchText)
    {
        if (string.IsNullOrEmpty(searchText)) return;

        var terms = searchText.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var term in terms)
        {
            var temp = term.Trim();
            if (string.IsNullOrEmpty(temp)) continue;

            Query.Where(c =>
                c.ClaveCredito.Contains(temp) ||
                (c.FI_Persona != null && c.FI_Persona.PrimerNombre!= null && c.FI_Persona.PrimerNombre.Contains(temp)) ||
                (c.FI_Persona != null && c.FI_Persona.SegundoNombre!= null && c.FI_Persona.SegundoNombre.Contains(temp)) ||
                (c.FI_Persona != null && c.FI_Persona.ApellidoPaterno != null && c.FI_Persona.ApellidoPaterno.Contains(temp)) ||
                (c.FI_Persona != null && c.FI_Persona.ApellidoMaterno!= null && c.FI_Persona.ApellidoMaterno.Contains(temp)) ||
                (c.FI_Persona != null && c.FI_Persona.CURP != null && c.FI_Persona.CURP.Contains(temp)) ||
                (c.FI_Persona != null && c.FI_Persona.RFC != null && c.FI_Persona.RFC.Contains(temp))
            );
        }
    }

    private void ApplyEstatusFilter(int? estatusCreditoId)
    {
        if (estatusCreditoId.HasValue && estatusCreditoId.Value != 0)
        {
            Query.Where(c => c.EstatusCreditoId == estatusCreditoId.Value);
        }
    }

    private void ApplyProductoFilter(int? productoId)
    {
        if (productoId.HasValue && productoId.Value != 0)
        {
            Query.Where(c => c.ProductoId == productoId.Value);
        }
    }

    private void ApplyFechaActivacionFilter(DateTime? start, DateTime? end)
    {
        if (start.HasValue)
        {
            Query.Where(c => c.FechaActivacion >= start.Value);
        }

        if (end.HasValue)
        {
            Query.Where(c => c.FechaActivacion <= end.Value);
        }
    }
}
