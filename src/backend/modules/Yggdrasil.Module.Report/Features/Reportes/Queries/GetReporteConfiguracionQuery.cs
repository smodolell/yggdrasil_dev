using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SqlKata.Compilers;
using SqlKata.Execution;
using Yggdrasil.Module.Report.Features.Reportes.DTOs;
using Yggdrasil.Module.Report.Constants;

namespace Yggdrasil.Module.Report.Features.Reportes.Queries;

public class GetReporteConfiguracionQuery : IQuery<Result<ReporteExecuteDto>>
{
    public int ReporteId { get; set; }
}

internal class GetReporteConfiguracionQueryHandler(
    IApplicationDbContext context,
    IConfiguration configuration
) : IQueryHandler<GetReporteConfiguracionQuery, Result<ReporteExecuteDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IConfiguration _configuration = configuration;

    public async Task<Result<ReporteExecuteDto>> HandleAsync(GetReporteConfiguracionQuery message, CancellationToken cancellationToken = default)
    {
        var oReporte = await _context.RSP_Reporte
            .Include(i => i.RSP_Parametro)
                .ThenInclude(t => t.RSP_Input)
            .SingleOrDefaultAsync(r => r.Id == message.ReporteId, cancellationToken);

        if (oReporte == null)
            return Result.NotFound("El reporte no existe.");

        var result = new ReporteExecuteDto
        {
            ReporteId = oReporte.Id,
            NomReporte = string.Format("{0}_{1:yyyy_MM_dd_hh_mm}", oReporte.NomReporte.Replace(' ', '_'), DateTime.Now),
            Titulo = oReporte.NomReporte,
            ReporteFormatoId = oReporte.ReporteFormatoId ?? 0
        };

        result.Parametros = oReporte.RSP_Parametro
            .OrderBy(o => o.Order)
            .Select(r => new ReporteExecuteParametroDto
            {
                ParametroId = r.Id,
                InputId = r.InputId,
                NomParametro = r.NomParametro,
                TipoDato = r.TipoDato,
                TablaRef = r.TablaRef,
                ColumnaValor = r.ColumnaValor,
                ColumnaTexto = r.ColumnaTexto,
                Display = r.Display,
                Order = r.Order

            }).ToList();

        foreach (var param in result.Parametros)
        {
            switch (param.TipoDato)
            {
                case "date":
                case "datetime":
                case "datetime2":
                    param.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    break;
            }

            param.DropDownList = param.InputId == PluginConstants.RSP_Input_DropDownList
                ? GetSelectList(param.TablaRef, param.ColumnaValor, param.ColumnaTexto)
                : [];
        }

        return Result.Success(result);
    }

    private List<SelectListItemDto> GetSelectList(string tabla, string columnaValor, string columnaTexto)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        using var connection = new SqlConnection(connectionString);
        var compiler = new SqlServerCompiler();
        var db = new QueryFactory(connection, compiler);

        var queryResult = db.Query(tabla)
            .Select($"{columnaValor} as Value", $"{columnaTexto} as Text")
            .Get();

        return queryResult.Select(s => new SelectListItemDto
        {
            Value = ((object)s.Value).ToString() ?? "",
            Text = (string)s.Text
        }).ToList();
    }
}
