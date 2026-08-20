namespace Yggdrasil.Module.Credito.CS.UI.Validators.Creditos;

public class CreditoCSEditDtoValidator : AbstractValidator<CreditoCSEditDto>
{
    public CreditoCSEditDtoValidator()
    {
        RuleFor(r => r.TipoCreditoId)
            .GreaterThan(0)
            .WithName("Tipo de Crédito");

        RuleFor(r => r.PeriodicidadId)
            .GreaterThan(0)
            .WithName("Periodicidad");

        RuleFor(r => r.MetodoArmotizacionId)
            .GreaterThan(0)
            .WithName("Método de Amortización");

        RuleFor(r => r.FechaInicio)
            .NotEqual(default(DateTime))
            .WithName("Fecha de Inicio");

        RuleFor(r => r.FechaPrimeraRenta)
            .NotEqual(default(DateTime))
            .WithName("Fecha de Primera Renta")
            .GreaterThanOrEqualTo(r => r.FechaInicio)
            .WithMessage("La fecha de primera renta debe ser posterior o igual a la fecha de inicio");

        RuleFor(r => r.Capital)
            .GreaterThan(0)
            .WithName("Capital");

        RuleFor(r => r.Tasa)
            .GreaterThanOrEqualTo(0)
            .WithName("Tasa");

        RuleFor(r => r.TasaIva)
            .GreaterThanOrEqualTo(0)
            .WithName("Tasa IVA");

        RuleFor(r => r.Plazo)
            .GreaterThan(0)
            .WithName("Plazo");

        RuleFor(r => r.ExcelFileBytes)
            .NotNull()
            .Must(bytes => bytes.Length > 0)
            .WithName("Archivo Excel")
            .WithMessage("Debe adjuntar un archivo Excel para este método de amortización")
            .When(r => r.EsImportacionExcel);
    }
}
