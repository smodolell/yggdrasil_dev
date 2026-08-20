using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Module.Report.Features.Reportes.Specifications;

public class ParametroSpec : Specification<RSP_Parametro>
{
    public ParametroSpec(int? reporteId = null, string? searchText = null)
    {
        if (reporteId.HasValue && reporteId.Value != 0)
        {
            Query.Where(p => p.ReporteId == reporteId.Value);
        }

        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p => p.NomParametro.Contains(searchText));
        }
    }
}
