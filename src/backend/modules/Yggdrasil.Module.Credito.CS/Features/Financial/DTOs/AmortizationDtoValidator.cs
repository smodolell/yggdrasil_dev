namespace Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;

public class AmortizationDtoValidator :AbstractValidator<AmortizationDto>
{
    public AmortizationDtoValidator()
    {
        RuleFor(x => x.SaldoInicial)
            .GreaterThan(0).WithMessage("El saldo inicial debe ser mayor a 0");

        RuleFor(x => x.Plazo)
            .GreaterThan(0).WithMessage("El plazo debe ser mayor a 0");

        RuleFor(x => x.FecPrimeraRenta)
            .GreaterThan(x => x.FecInicioContrato)
            .WithMessage("La fecha de primera renta debe ser posterior a la fecha de inicio");

        RuleFor(x => x.TasaAnual)
            .GreaterThanOrEqualTo(0).WithMessage("La tasa mensual no puede ser negativa");

        RuleFor(x => x.TasaIVA)
            .InclusiveBetween(0, 1).WithMessage("La tasa IVA debe estar entre 0 y 1");
    }
}

