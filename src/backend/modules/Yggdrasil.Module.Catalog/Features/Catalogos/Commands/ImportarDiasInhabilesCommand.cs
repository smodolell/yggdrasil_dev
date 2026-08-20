using ClosedXML.Excel;
using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;


public class ImportarDiasInhabilesCommand : ICommand<Result>
{
    public required Stream ArchivoStream { get; set; }
}

internal class SubirCalendarioLaboralCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<ImportarDiasInhabilesCommand, Result>
{
    public async Task<Result> HandleAsync(ImportarDiasInhabilesCommand request, CancellationToken cancellationToken = default)
    {
        try
        {
            // Procesar el archivo Excel
            var processResult = await ProcesarArchivoExcel(request.ArchivoStream);

            if (!processResult.IsSuccess)
                return Result.Error("Error al procesar el archivo Excel");

            var diasInhabiles = processResult.Value;

            if (diasInhabiles == null || !diasInhabiles.Any())
                return Result.Error("El archivo no contiene días inhábiles válidos");
         
            // Marcar los días inhábiles
            foreach (var diaInhabil in diasInhabiles)
            {
                var entity = await context.CAT_CalendarioLaboral.FirstOrDefaultAsync(x => x.Fecha.Date == diaInhabil.Fecha);
                if (entity == null)
                    continue;
                entity.EsHabil = false;
                
                if(!string.IsNullOrEmpty(diaInhabil.Descripcion))
                {
                    entity.Descripcion = diaInhabil.Descripcion ?? string.Empty;
                }

                context.CAT_CalendarioLaboral.Update(entity);
                await context.SaveChangesAsync(cancellationToken);

            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error($"Error al sincronizar el calendario: {ex.Message}");
        }
    }


    public async Task<Result<List<CalendarioLaboralExcelDto>>> ProcesarArchivoExcel(Stream fileStream)
    {
        try
        {
            var result = new List<CalendarioLaboralExcelDto>();

            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheet(1);

            // Obtener el rango usado
            var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // Saltar encabezados

            foreach (var row in rows)
            {
                var fechaCell = row.Cell(1);
                var descripcionCell = row.Cell(2);

                // Validar que la fecha no esté vacía
                if (string.IsNullOrWhiteSpace(fechaCell.GetString()))
                    continue;

                try
                {
                    var fecha = fechaCell.GetDateTime();

                    // Validar que la fecha sea válida
                    if (fecha.Year < 2000 || fecha.Year > 2100)
                        continue;

                    var descripcion = descripcionCell.GetString().Trim();

                    result.Add(new CalendarioLaboralExcelDto
                    {
                        Fecha = fecha,
                        Descripcion = descripcion
                    });
                }
                catch
                {
                    // Si no se puede convertir la fecha, continuar
                    continue;
                }
            }

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error($"Error al procesar el archivo: {ex.Message}");
        }
    }
}
