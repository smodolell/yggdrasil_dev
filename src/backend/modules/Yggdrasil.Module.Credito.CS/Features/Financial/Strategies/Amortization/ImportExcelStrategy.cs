using ClosedXML.Excel;
using Yggdrasil.Module.Credito.CS.Features.Financial.Attibutes;
using Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;
using Yggdrasil.Module.Credito.CS.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Strategies.Amortization;

[AmortizationMethod(AmortizationMethod.ImportExcel)]
public class ImportExcelStrategy : AbstractAmortizationStrategy
{
    protected override string StrategyName => "Importación desde Excel";

    // Configuración de columnas esperadas
    private const string COL_FECHA_INICIO = "FechaInicio";
    private const string COL_FECHA_VENCIMIENTO = "FechaVencimiento";
    private const string COL_SALDO_INICIAL = "SaldoInicial";
    private const string COL_CAPITAL = "Capital";
    private const string COL_INTERES = "Interes";
    private const string COL_IVA = "Iva";

    public override Result<AmortizationResultDto> Calculate(AmortizationDto request, List<DateTime> fechas)
    {
        try
        {
            ValidarRequest(request);

            // Verificar que tengamos los datos del archivo Excel en memoria
            if (request.ExcelFileBytes == null || request.ExcelFileBytes.Length == 0)
            {
                return CrearResultadoError("No se proporcionaron los datos del archivo Excel para importar.");
            }

            // Importar datos desde el arreglo de bytes
            var resultados = ImportarDesdeBytes(request.ExcelFileBytes);

            if (resultados == null || resultados.Count == 0)
            {
                return CrearResultadoError("No se encontraron datos válidos en el archivo Excel.");
            }

            // Validar que los datos importados sean consistentes
            var validacion = ValidarDatosImportados(resultados, request);
            if (!validacion.IsSuccess)
            {
                //return CrearResultadoError(string.Join(",", validacion.Errors));
                return CrearResultadoError(validacion.ValidationErrors.ToArray());
            }

            // Crear el resultado
            var resultDto = new AmortizationResultDto
            {
                TablaAmortiza = resultados,
                EsImportada = true,
                NombreArchivo = request.NombreArchivoExcel
            };

            return Result.Success(resultDto);
        }
        catch (Exception ex)
        {
            return CrearResultadoError($"Error en estrategia de importación Excel: {ex.Message}");
        }
    }

    private List<AmortizacionRow> ImportarDesdeBytes(byte[] excelBytes)
    {
        var resultados = new List<AmortizacionRow>();

        using (var stream = new MemoryStream(excelBytes))
        using (var workbook = new XLWorkbook(stream))
        {
            // Asumimos que los datos están en la primera hoja
            var worksheet = workbook.Worksheet(1);

            if (worksheet == null)
            {
                throw new Exception("No se encontró ninguna hoja en el archivo Excel.");
            }

            // Obtener el rango usado
            var usedRange = worksheet.RangeUsed();
            if (usedRange == null)
            {
                throw new Exception("El archivo Excel está vacío.");
            }

            // Mapear columnas por nombre (fila 1 es el encabezado)
            var headerRow = worksheet.Row(1);
            var columnMap = MapearColumnas(headerRow);

            // Validar que todas las columnas requeridas existan
            ValidarColumnasRequeridas(columnMap);

            // Obtener la última fila con datos
            int lastRow = usedRange.LastRow().RowNumber();

            // Iterar sobre las filas de datos (comenzando desde la fila 2)
            for (int row = 2; row <= lastRow; row++)
            {
                try
                {
                    var fila = new AmortizacionRow
                    {
                        NoPago = row - 1, // Número de pago basado en la fila
                        IdTipoTabla = 1,
                        EsValorResidual = false
                    };

                    // Leer fecha de inicio
                    var fechaInicioCell = worksheet.Cell(row, columnMap[COL_FECHA_INICIO]);
                    if (fechaInicioCell != null && !fechaInicioCell.IsEmpty())
                    {
                        if (DateTime.TryParse(fechaInicioCell.GetValue<string>(), out DateTime fechaInicio))
                        {
                            fila.FecInicio = fechaInicio;
                        }
                        else if (fechaInicioCell.DataType == XLDataType.DateTime)
                        {
                            fila.FecInicio = fechaInicioCell.GetValue<DateTime>();
                        }
                    }

                    // Leer fecha de vencimiento
                    var fechaVenCell = worksheet.Cell(row, columnMap[COL_FECHA_VENCIMIENTO]);
                    if (fechaVenCell != null && !fechaVenCell.IsEmpty())
                    {
                        if (DateTime.TryParse(fechaVenCell.GetValue<string>(), out DateTime fechaVencimiento))
                        {
                            fila.FecVencimiento = fechaVencimiento;
                            fila.FecFinal = fechaVencimiento;
                        }
                        else if (fechaVenCell.DataType == XLDataType.DateTime)
                        {
                            fila.FecVencimiento = fechaVenCell.GetValue<DateTime>();
                            fila.FecFinal = fila.FecVencimiento;
                        }
                    }

                    // Calcular días si tenemos ambas fechas
                    if (fila.FecInicio != default && fila.FecVencimiento != default)
                    {
                        fila.Dias = (fila.FecVencimiento - fila.FecInicio).Days;
                    }

                    // Leer valores numéricos
                    fila.SaldoInicial = ObtenerValorDecimal(worksheet.Cell(row, columnMap[COL_SALDO_INICIAL]));
                    fila.Capital = ObtenerValorDecimal(worksheet.Cell(row, columnMap[COL_CAPITAL]));
                    fila.Interes = ObtenerValorDecimal(worksheet.Cell(row, columnMap[COL_INTERES]));
                    fila.IVA = ObtenerValorDecimal(worksheet.Cell(row, columnMap[COL_IVA]));

                    // Calcular total mensual
                    fila.Total = fila.Capital + fila.Interes + fila.IVA;

                    // Calcular saldo final
                    fila.SaldoFinal = fila.SaldoInicial - fila.Capital;

                    resultados.Add(fila);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al procesar la fila {row}: {ex.Message}");
                }
            }
        }

        return resultados;
    }

    private static Dictionary<string, int> MapearColumnas(IXLRow headerRow)
    {
        var columnMap = new Dictionary<string, int>();

        // Recorrer todas las columnas usadas en el encabezado
        foreach (var cell in headerRow.CellsUsed())
        {
            var headerValue = cell.GetValue<string>()?.Trim();

            if (!string.IsNullOrEmpty(headerValue))
            {
                // Mapear según el nombre de columna esperado (case insensitive)
                switch (headerValue.ToLower())
                {
                    case "fechainicio":
                    case "fecha inicio":
                    case "fecha_incio":
                    case "fecha de inicio":
                        columnMap[COL_FECHA_INICIO] = cell.Address.ColumnNumber;
                        break;
                    case "fechavencimiento":
                    case "fecha vencimiento":
                    case "fecha_vencimiento":
                    case "fechavenc":
                    case "fecha de vencimiento":
                        columnMap[COL_FECHA_VENCIMIENTO] = cell.Address.ColumnNumber;
                        break;
                    case "saldoinicial":
                    case "saldo inicial":
                    case "saldo_incial":
                    case "saldo":
                        columnMap[COL_SALDO_INICIAL] = cell.Address.ColumnNumber;
                        break;
                    case "capital":
                    case "amortizacion":
                    case "amortización":
                    case "capital amortizado":
                        columnMap[COL_CAPITAL] = cell.Address.ColumnNumber;
                        break;
                    case "interes":
                    case "interés":
                    case "interes_calculado":
                    case "interés calculado":
                        columnMap[COL_INTERES] = cell.Address.ColumnNumber;
                        break;
                    case "iva":
                    case "i.v.a.":
                    case "impuesto":
                    case "iva_interes":
                    case "iva interés":
                    case "iva sobre intereses":
                        columnMap[COL_IVA] = cell.Address.ColumnNumber;
                        break;
                }
            }
        }

        return columnMap;
    }

    private static void ValidarColumnasRequeridas(Dictionary<string, int> columnMap)
    {
        var columnasRequeridas = new[] {
            COL_FECHA_INICIO,
            COL_FECHA_VENCIMIENTO,
            COL_SALDO_INICIAL,
            COL_CAPITAL,
            COL_INTERES,
            COL_IVA
        };

        var columnasFaltantes = columnasRequeridas
            .Where(col => !columnMap.ContainsKey(col))
            .ToList();

        if (columnasFaltantes.Any())
        {
            throw new Exception($"Faltan las siguientes columnas requeridas: {string.Join(", ", columnasFaltantes)}");
        }
    }

    private static decimal ObtenerValorDecimal(IXLCell cell)
    {
        if (cell == null || cell.IsEmpty())
            return 0m;

        try
        {
            // Intentar obtener como número directamente
            if (cell.DataType == XLDataType.Number)
            {
                return Math.Round(cell.GetValue<decimal>(), 2, MidpointRounding.AwayFromZero);
            }

            // Intentar parsear desde string
            var stringValue = cell.GetValue<string>()?.Trim();
            if (!string.IsNullOrEmpty(stringValue))
            {
                // Reemplazar posibles separadores de miles y decimales
                stringValue = stringValue.Replace(",", ".");
                if (decimal.TryParse(stringValue, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal resultado))
                {
                    return Math.Round(resultado, 2, MidpointRounding.AwayFromZero);
                }
            }

            return 0m;
        }
        catch
        {
            return 0m;
        }
    }

    private  Result ValidarDatosImportados(List<AmortizacionRow> resultados, AmortizationDto request)
    {
        // Validar que todas las filas tengan fechas válidas
        var filasSinFecha = resultados
            .Where(r => r.FecInicio == default || r.FecVencimiento == default)
            .ToList();

        if (filasSinFecha.Any())
        {
            return Result.Invalid( new ValidationError($"Existen {filasSinFecha.Count} filas con fechas inválidas."));
        }

        // Validar que el número de filas coincida con el plazo
        if (request.Plazo > 0 && resultados.Count != request.Plazo)
        {
            return Result.Invalid(new ValidationError($"El número de filas importadas ({resultados.Count}) no coincide con el plazo especificado ({request.Plazo})."));
        }

        // Validar que el saldo inicial coincida con el capital financiado
        if (resultados.Any() && Math.Abs(resultados.First().SaldoInicial - request.SaldoInicial) > 0.01m)
        {
            var m = $"El saldo inicial en el Excel ({resultados.First().SaldoInicial:C2}) no coincide con el capital financiado ({request.SaldoInicial:C2}).";
            return Result.Invalid(new ValidationError(m));
        }

        // Validar secuencia de fechas
        for (int i = 1; i < resultados.Count; i++)
        {
            var fechaAnterior = resultados[i - 1].FecVencimiento.Date;
            var fechaActualInicio = resultados[i].FecInicio.Date;

            if (fechaActualInicio != fechaAnterior)
            {
                return Result.Invalid(new ValidationError($"La fecha de inicio de la fila {i + 1} ({resultados[i].FecInicio:dd/MM/yyyy}) no coincide con la fecha de vencimiento de la fila anterior ({resultados[i - 1].FecVencimiento:dd/MM/yyyy})."));
            }
        }

        // Validar saldos consistentes
        for (int i = 1; i < resultados.Count; i++)
        {
            var filaAnterior = resultados[i - 1];
            var filaActual = resultados[i];

            if (Math.Abs(filaActual.SaldoInicial - filaAnterior.SaldoFinal) > 0.01m)
            {
                return Result.Invalid(new ValidationError($"El saldo inicial de la fila {i + 1} ({filaActual.SaldoInicial:C2}) no coincide con el saldo final de la fila anterior ({filaAnterior.SaldoFinal:C2})."));
            }
        }

        // Validar que el último saldo final sea 0 (o cercano a 0)
        var ultimo = resultados.Last();
        if (Math.Abs(ultimo.SaldoFinal) > 0.01m)
        {
            return Result.Invalid(new ValidationError($"El saldo final del último periodo ({ultimo.SaldoFinal:C2}) debería ser 0."));
        }

        // Validar que no haya valores negativos
        if (resultados.Any(r => r.Capital < 0 || r.Interes < 0 || r.IVA < 0))
        {
            return Result.Invalid(new ValidationError("Existen valores negativos en la tabla importada."));
        }

        // Validar que el total de capital amortizado sea igual al saldo inicial
        var totalCapital = resultados.Sum(r => r.Capital);
        if (Math.Abs(totalCapital - request.SaldoInicial) > 0.01m)
        {
            return Result.Invalid(new ValidationError($"El total de capital amortizado ({totalCapital:C2}) no coincide con el saldo inicial ({request.SaldoInicial:C2})."));
        }

        return Result.Success();
    }
}
