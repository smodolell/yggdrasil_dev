using Yggdrasil.Module.Report.Constants;

namespace Yggdrasil.Module.Report.Features.Reportes.DTOs;

public class ReporteExportDto
{
    public byte[] Data { get; set; } = [];
    public string ContentType { get; set; } = PluginConstants.ContentType_Excel;
    public string FileName { get; set; } = "";
}
