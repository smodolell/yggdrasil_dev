namespace Yggdrasil.Module.Credito.UI.Services.Creditos.DTOs;

public class CreditoEditDtoValidator : AbstractValidator<CreditoEditDto>
{
    public CreditoEditDtoValidator()
    {
        RuleFor(r => r.Capital)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("El capital debe ser mayor a 0");

        RuleFor(r => r.MonedaId)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("La moneda es requerida");

        RuleFor(r => r.Plazo)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("El plazo debe ser mayor a 0");

        RuleFor(r => r.PeriodicidadId)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("La periodicidad es requerida");

        RuleFor(r => r.Tasa)
            .NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .WithMessage("La tasa debe estar entre 1 y 100 porciento");

        RuleFor(r => r.TasaIva)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100)
            .WithMessage("El IVA debe estar entre 0 y 100 porciento");

        RuleFor(r => r)
            .Must(r => !r.FechaInicio.HasValue || !r.FechaPrimeraRenta.HasValue || r.FechaInicio <= r.FechaPrimeraRenta)
            .WithMessage("La fecha de inicio debe ser menor o igual a la fecha de primera renta");
    }
}
