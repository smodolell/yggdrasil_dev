namespace Yggdrasil.Module.Audit.UI.Services.Audits.DTOs;

public class AuditReportDto
{
    public int? Anio { get; set; }
    public int? Mes { get; set; }
    public string? UserName { get; set; }
    public List<AuditReportColumnDto> Columnas { get; set; } = [];
    public List<string> Usuarios { get; set; } = [];
    public List<AuditReportItemDto> Items { get; set; } = [];
}

public class AuditReportItemDto
{
    public string UserName { get; set; } = "";
    public int AuditEventId { get; set; }
    public int Cantidad { get; set; }
}

public class AuditReportColumnDto
{
    public int AuditEventId { get; set; }
    public string AuditEvent { get; set; } = "";
}
