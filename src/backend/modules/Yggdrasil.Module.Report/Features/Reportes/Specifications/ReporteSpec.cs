using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Module.Report.Features.Reportes.Specifications;

public class ReporteSpec : Specification<RSP_Reporte>
{
    public ReporteSpec(string? searchText = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p =>
                p.NomReporte.Contains(searchText) ||
                p.StoredProcedure.Contains(searchText)
            );
        }
    }
}
