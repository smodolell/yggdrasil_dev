using ClosedXML.Excel;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Queries;

public class GetLayoutCalendarioQuery : IQuery<Result<FileDownloadDto>>
{
}

internal class GetLayoutCalendarioQueryHandler(
) : IQueryHandler<GetLayoutCalendarioQuery, Result<FileDownloadDto>>
{
    public async Task<Result<FileDownloadDto>> HandleAsync(GetLayoutCalendarioQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var fileInfo = GenerarLayoutCalendarioLaboral();
            return Result.Success(fileInfo);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error al generar el layout: {ex.Message}");
        }
    }

    public static FileDownloadDto GenerarLayoutCalendarioLaboral()
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Calendario Laboral");

        // Configurar encabezados
        worksheet.Cell(1, 1).Value = "Fecha";
        worksheet.Cell(1, 2).Value = "Descripción";

        // Estilo de encabezados
        var headerRange = worksheet.Range(1, 1, 1, 2);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
        headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

        // Configurar ancho de columnas
        worksheet.Column(1).Width = 15; // Fecha
        worksheet.Column(2).Width = 40; // Descripción

        // Agregar ejemplo de datos
        var fechaEjemplo = new DateTime(DateTime.Now.Year, 11, 19);
        worksheet.Cell(2, 1).Value = fechaEjemplo;
        worksheet.Cell(2, 2).Value = "Ejemplo: Dia del Hombre";

        // Configurar formato de fecha
        worksheet.Column(1).Style.DateFormat.Format = "dd/MM/yyyy";

        // Agregar instrucciones
        worksheet.Cell(1, 3).Value = "INSTRUCCIONES:";
        worksheet.Cell(2, 3).Value = "1. Complete SOLO los días que son inhábiles";
        worksheet.Cell(3, 3).Value = "2. No incluya días hábiles, estos ya están configurados";
        worksheet.Cell(4, 3).Value = "3. La descripción es opcional pero recomendada";
        worksheet.Cell(5, 3).Value = "4. Ejemplos: feriados, días de asueto, etc.";
        worksheet.Cell(6, 3).Value = "5. Formato de fecha: DD/MM/AAAA";

        // Aplicar bordes a la tabla
        var tableRange = worksheet.Range(1, 1, 2, 3);
        tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var bytes = stream.ToArray();

        // Crear el nombre del archivo con timestamp
        var fileName = $"Layout_Calendario_Laboral_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

        return new FileDownloadDto(
            bytes,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileName
        );
    }
}