namespace Yggdrasil.Module.Credito.Features.Operaciones.CapturaCredito.DTOs;

public class CreditoEditDtoValidator : AbstractValidator<CreditoEditDto>
{
    public CreditoEditDtoValidator(IApplicationDbContext context)
    {
        RuleFor(r => r.Capital)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("El capital debe ser mayor a 0");

        RuleFor(r => r.MonedaId)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("La moneda es requerida")
            .MustAsync(async (monedaId, cancellation) =>
            {
                return await context.CAT_Moneda
                    .AnyAsync(m => m.Id == monedaId, cancellation);
            })
            .WithMessage("La moneda especificada no existe");

        RuleFor(r => r.Plazo)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("El plazo debe ser mayor a 0");

        RuleFor(r => r.PeriodicidadId)
            .NotNull()
            .GreaterThan(0)
            .WithMessage("La periodicidad es requerida")
            .MustAsync(async (periodicidadId, cancellation) =>
            {
                return await context.CAT_Periodicidad
                    .AnyAsync(p => p.Id == periodicidadId, cancellation);
            })
            .WithMessage("La periodicidad especificada no existe");

        // Validación adicional para Tasa
        RuleFor(r => r.Tasa)
            .NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .WithMessage("La tasa debe estar entre 1 y 100 porciento");

        // Validación para TasaIVA
        RuleFor(r => r.TasaIva)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100)
            .WithMessage("El IVA debe estar entre 0 y 100 porciento");

        // Validación condicional: FechaInicio vs FechaFin
        RuleFor(r => r)
            .Must(r => !r.FechaInicio.HasValue || !r.FechaPrimeraRenta.HasValue || r.FechaInicio <= r.FechaPrimeraRenta)
            .WithMessage("La fecha de inicio debe ser menor o igual a la fecha de primera renta");
    }
}

