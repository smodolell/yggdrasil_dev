using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Module.Report.Features.Reportes.Specifications;

public class ArchivoSpec : Specification<RSP_Archivo>
{
    public ArchivoSpec(int? reporteId = null)
    {
        if (reporteId.HasValue && reporteId.Value != 0)
        {
            Query.Where(p => p.ReporteId == reporteId.Value);
        }
    }
}
