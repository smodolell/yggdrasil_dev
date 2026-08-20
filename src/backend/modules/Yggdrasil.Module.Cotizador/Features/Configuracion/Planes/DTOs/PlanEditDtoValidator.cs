namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

public class PlanEditDtoValidator : AbstractValidator<PlanEditDto>
{
    public PlanEditDtoValidator()
    {
        RuleFor(x => x.ProductoId)
            .NotNull()
            .WithName("Producto");

        RuleFor(r => r.NomPlan)
            .NotEmpty()
            .MaximumLength(80)
            .WithName("Nombre del Plan");

        RuleFor(r => r.Descripcion)
            .MaximumLength(200)
            .WithName("Descripción");

        RuleFor(x => x.ImporteMinimo)
            .Must((dto, importeMinimo) => importeMinimo <= dto.ImporteMaximo)
            .WithMessage("El Importe Minimo no debe ser mayor que el Importe Maximo.");

        RuleFor(x => x.EdadMinima)
            .Must((dto, edadMinima) => edadMinima <= dto.EdadMaxima)
            .WithMessage("El Edad Minima no debe ser mayor que el Edad Maxima.");

        RuleFor(plan => plan.Periodicidades)
            .Must(lista => lista != null && lista.Any(item => item.Activo))
            .WithMessage("Seleccione al menos una periodicidad");
    }
}