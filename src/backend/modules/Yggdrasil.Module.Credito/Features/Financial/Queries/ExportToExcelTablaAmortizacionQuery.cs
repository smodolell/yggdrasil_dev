using ClosedXML.Excel;
using Yggdrasil.Module.Credito.Features.Financial.DTOs;

namespace Yggdrasil.Module.Credito.Features.Financial.Queries;

public record ExportToExcelTablaAmortizacionQuery(AmortizationResultDto Model) : IQuery<Result<byte[]>>;

internal class ExportToExcelTablaAmortizacionQueryHandler : IQueryHandler<ExportToExcelTablaAmortizacionQuery, Result<byte[]>>
{
    public Task<Result<byte[]>> HandleAsync(ExportToExcelTablaAmortizacionQuery message, CancellationToken cancellationToken = default)
    {
        try
        {
            var excelBytes = GenerateExcel(message.Model);
            return Task.FromResult(Result<byte[]>.Success(excelBytes));
        }
        catch (Exception ex)
        {
            return Task.FromResult(Result<byte[]>.Error($"Error al generar Excel: {ex.Message}"));
        }
    }

    private byte[] GenerateExcel(AmortizationResultDto model)
    {
        using var workbook = new XLWorkbook();

        // Una sola hoja con toda la información
        var worksheet = workbook.Worksheets.Add("Tabla de Amortización");

        // Configurar estilos
        var moneyStyle = "#,##0.00";
        var dateStyle = "dd/MM/yyyy";

        int currentRow = 1;

        // ========== SECCIÓN 1: TÍTULO PRINCIPAL ==========
        worksheet.Cell(currentRow, 1).Value = $"TABLA DE AMORTIZACIÓN - MÉTODO {model.Method.ToString().ToUpper()}";
        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        worksheet.Cell(currentRow, 1).Style.Font.FontSize = 16;
        worksheet.Range(currentRow, 1, currentRow, 10).Merge();
        worksheet.Range(currentRow, 1, currentRow, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        currentRow += 2;

        // ========== SECCIÓN 2: RESUMEN DEL CRÉDITO ==========
        worksheet.Cell(currentRow, 1).Value = "RESUMEN DEL CRÉDITO";
        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        worksheet.Cell(currentRow, 1).Style.Font.FontSize = 12;
        worksheet.Range(currentRow, 1, currentRow, 10).Merge();
        worksheet.Range(currentRow, 1, currentRow, 10).Style.Fill.BackgroundColor = XLColor.LightGray;
        currentRow++;

        // Datos del resumen en formato de 2 columnas
        var resumeData = new Dictionary<string, object>
        {
            ["Método de Amortización"] = model.Method.ToString(),
            ["Fecha de Generación"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
            ["Fecha de Inicio"] = model.FechaInicio.ToString("dd/MM/yyyy"),
            ["Fecha Primera Renta"] = model.FecPrimeraRenta.ToString("dd/MM/yyyy"),
            ["Saldo Inicial"] = model.SaldoInicial,
            ["Plazo"] = $"{model.Plazo} meses",
            ["Tasa Anual"] = $"{model.TasaAnual}%",
            ["Tasa IVA"] = $"{model.TasaIVA}%",
            ["Genera IVA sobre Interés"] = model.GeneraIVAInteres ? "Sí" : "No"
        };

        foreach (var item in resumeData)
        {
            worksheet.Cell(currentRow, 1).Value = item.Key;
            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 2).Value = item.Value.ToString();

            if (item.Value is decimal decimalValue)
            {
                worksheet.Cell(currentRow, 2).Style.NumberFormat.Format = moneyStyle;
            }

            currentRow++;
        }

        currentRow++;

        // ========== SECCIÓN 3: TOTALES ==========
        worksheet.Cell(currentRow, 1).Value = "TOTALES DEL CRÉDITO";
        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        worksheet.Cell(currentRow, 1).Style.Font.FontSize = 12;
        worksheet.Range(currentRow, 1, currentRow, 10).Merge();
        worksheet.Range(currentRow, 1, currentRow, 10).Style.Fill.BackgroundColor = XLColor.LightGray;
        currentRow++;

        var totalsData = new Dictionary<string, decimal>
        {
            ["Total Capital Amortizado"] = model.TotalCapital,
            ["Total Intereses Pagados"] = model.TotalInteres,
            ["Total IVA Pagado"] = model.TotalIVA,
            ["Total Pagado"] = model.TotalPagado
        };

        foreach (var item in totalsData)
        {
            worksheet.Cell(currentRow, 1).Value = item.Key;
            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 2).Value = item.Value;
            worksheet.Cell(currentRow, 2).Style.NumberFormat.Format = moneyStyle;

            // Resaltar total pagado
            if (item.Key == "Total Pagado")
            {
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Style.Font.FontColor = XLColor.Green;
            }

            currentRow++;
        }

        currentRow += 2;

        // ========== SECCIÓN 4: TABLA DE AMORTIZACIÓN ==========
        worksheet.Cell(currentRow, 1).Value = "DETALLE DE PAGOS";
        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        worksheet.Cell(currentRow, 1).Style.Font.FontSize = 12;
        worksheet.Range(currentRow, 1, currentRow, 10).Merge();
        worksheet.Range(currentRow, 1, currentRow, 10).Style.Fill.BackgroundColor = XLColor.LightGray;
        currentRow++;

        // Headers de la tabla
        var headers = new[] { "No. Pago", "Fec. Inicio", "Fec. Vencimiento", "Días",
                              "Saldo Inicial", "Capital", "Interés", "IVA", "Total", "Saldo Final" };

        for (int col = 0; col < headers.Length; col++)
        {
            var cell = worksheet.Cell(currentRow, col + 1);
            cell.Value = headers[col];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.LightBlue;
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            cell.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        }
        currentRow++;

        // Filtrar solo registros detalle (IdTipoTabla = 1)
        var tablaDetalle = model.TablaAmortiza.Where(x => x.IdTipoTabla == 1).ToList();

        // Datos de la tabla
        foreach (var item in tablaDetalle)
        {
            worksheet.Cell(currentRow, 1).Value = item.NoPago;
            worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 2).Value = item.FecInicio;
            worksheet.Cell(currentRow, 2).Style.DateFormat.Format = dateStyle;

            worksheet.Cell(currentRow, 3).Value = item.FecVencimiento;
            worksheet.Cell(currentRow, 3).Style.DateFormat.Format = dateStyle;

            worksheet.Cell(currentRow, 4).Value = item.Dias;
            worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 5).Value = item.SaldoInicial;
            worksheet.Cell(currentRow, 5).Style.NumberFormat.Format = moneyStyle;

            worksheet.Cell(currentRow, 6).Value = item.Capital;
            worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = moneyStyle;

            worksheet.Cell(currentRow, 7).Value = item.Interes;
            worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = moneyStyle;

            worksheet.Cell(currentRow, 8).Value = item.IVA;
            worksheet.Cell(currentRow, 8).Style.NumberFormat.Format = moneyStyle;

            worksheet.Cell(currentRow, 9).Value = item.Total;
            worksheet.Cell(currentRow, 9).Style.NumberFormat.Format = moneyStyle;
            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;

            worksheet.Cell(currentRow, 10).Value = item.SaldoFinal;
            worksheet.Cell(currentRow, 10).Style.NumberFormat.Format = moneyStyle;

            // Formato condicional - resaltar cuando sea valor residual
            if (item.EsValorResidual)
            {
                var rowRange = worksheet.Range(currentRow, 1, currentRow, 10);
                rowRange.Style.Fill.BackgroundColor = XLColor.LightYellow;
            }

            currentRow++;
        }

        // Fila de totales de la tabla
        worksheet.Cell(currentRow, 5).Value = "TOTALES:";
        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        worksheet.Range(currentRow, 5, currentRow, 5).Merge();

        worksheet.Cell(currentRow, 6).Value = model.TotalCapital;
        worksheet.Cell(currentRow, 6).Style.NumberFormat.Format = moneyStyle;
        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;

        worksheet.Cell(currentRow, 7).Value = model.TotalInteres;
        worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = moneyStyle;
        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;

        worksheet.Cell(currentRow, 8).Value = model.TotalIVA;
        worksheet.Cell(currentRow, 8).Style.NumberFormat.Format = moneyStyle;
        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;

        worksheet.Cell(currentRow, 9).Value = model.TotalPagado;
        worksheet.Cell(currentRow, 9).Style.NumberFormat.Format = moneyStyle;
        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
        worksheet.Cell(currentRow, 9).Style.Font.FontColor = XLColor.Green;

        // Aplicar bordes a toda la tabla
        var firstDataRow = currentRow - tablaDetalle.Count;
        var tableRange = worksheet.Range(firstDataRow - 1, 1, currentRow, 10);
        tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
        tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

        // Aplicar bordes al resumen
        var summaryRange = worksheet.Range(4, 1, currentRow - tablaDetalle.Count - 4, 2);
        summaryRange.Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
        summaryRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

        // Autoajustar columnas
        worksheet.Columns().AdjustToContents();

        // Congelar paneles (mantener visibles el título, resumen y headers)
        worksheet.SheetView.FreezeRows(firstDataRow - 1);

        // Guardar a byte array
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}