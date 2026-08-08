namespace Yggdrasil.Module.Audit.Features.LoginLog.Specifications;

public class LoginLogSpec : Specification<SYS_LoginLog>
{
    public LoginLogSpec(
        string? searchText = null,
        bool? isSuccessd = null,
        DateTime? timeStart = null,
        DateTime? timeEnd = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p => p.UserName.Contains(searchText));
        }

        if (isSuccessd.HasValue)
        {
            if (isSuccessd.Value)
                Query.Where(p => p.IsSuccessd);
            else
                Query.Where(p => !p.IsSuccessd);
        }

        if (timeStart.HasValue)
        {
            Query.Where(p => p.Time.Date >= timeStart.Value.Date);
        }

        if (timeEnd.HasValue)
        {
            Query.Where(p => p.Time.Date <= timeEnd.Value.Date);
        }
    }
}
