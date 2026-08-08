namespace Yggdrasil.Module.Audit.Features.Audit.Specifications;

public class AuditReportSpec : Specification<SYS_Audit>
{
    public AuditReportSpec(int? anio = null, int? mes = null, string? userName = null)
    {
        var now = DateTime.Now;
        anio ??= now.Year;
        mes ??= now.Month;

        if (anio.HasValue && anio.Value != 0)
        {
            Query.Where(p => p.RegisteredDate.Year == anio.Value);
        }

        if (mes.HasValue && mes.Value != 0)
        {
            Query.Where(p => p.RegisteredDate.Month == mes.Value);
        }

        if (!string.IsNullOrEmpty(userName))
        {
            Query.Where(p => p.UserName.Contains(userName));
        }
    }
}
