namespace Yggdrasil.Common.DTOs;

public class ApplicationSettingDto
{
    public int ApplicationId { get; set; }
    public string ApplicationName { get; set; } = "";

    public ApplicationSettingDto(ApplicationOptions options)
    {
        ApplicationId = options.ApplicationId;
        ApplicationName = options.ApplicationName;
    }
}


public class ApplicationOptions
{
    public int ApplicationId { get; set; }
    public string ApplicationName { get; set; } = string.Empty;
}