using FluentValidation;
using Yggdrasil.ApiClient.Contracts;

namespace Yggdrasil.Module.Report.UI.Validators;

public class ReporteEditDtoValidator : AbstractValidator<ReporteEditDto>
{
    public ReporteEditDtoValidator()
    {
        RuleFor(r => r.NomReporte)
            .NotEmpty()
            .WithName("Nombre del Reporte");

        RuleFor(r => r.StoredProcedure)
            .NotEmpty()
            .WithName("Procedimiento");

        RuleFor(r => r.ReporteFormatoId)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .WithName("Formato Salida");
    }
}