namespace Yggdrasil.Module.Audit.Features.Audit.Specifications;

public class AuditSpec : Specification<SYS_Audit>
{
    public AuditSpec(
        string? searchText = null,
        int? auditEventId = null,
        bool? hasError = null,
        DateTime? registeredDateInicial = null,
        DateTime? registeredDateFinal = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            var terms = searchText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Query.Where(p =>
                terms.Any(temp =>
                    p.UserName.Contains(temp) ||
                    p.Message.Contains(temp)
                )
            );
        }

        if (auditEventId.HasValue && auditEventId.Value != 0)
        {
            Query.Where(p => p.AuditEventId == auditEventId.Value);
        }

        if (hasError.HasValue)
        {
            Query.Where(p => p.HasError == hasError.Value);
        }

        if (registeredDateInicial.HasValue)
        {
            Query.Where(p => p.RegisteredDate.Date >= registeredDateInicial.Value.Date);
        }

        if (registeredDateFinal.HasValue)
        {
            Query.Where(p => p.RegisteredDate.Date <= registeredDateFinal.Value.Date);
        }
    }
}
