namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

public class PlanCreateDtoValidation : AbstractValidator<PlanCreateDto>
{
    public PlanCreateDtoValidation()
    {
        RuleFor(x => x.ProductoId)
            .NotNull()
            .WithName("Producto");

        RuleFor(x => x.NomPlan)
            .NotEmpty()
            .NotNull()
            .MaximumLength(80)
            .WithName("Plan");

        RuleFor(x => x.Descripcion)
            .MaximumLength(200)
            .WithName("Descripción");

        RuleFor(x => x.TipoPersonas)
            .Must(items => items != null && items.Any(item => item.Activo))
            .WithMessage("Debe seleccionar al menos un Tipo de Persona");


    }
}