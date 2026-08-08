namespace Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

public class MonedaEditDto
{
    public string NomMoneda { get; set; } = "";
    public string ClaveMoneda { get; set; } = "";
    public bool PorDefecto { get; set; }
}

public class MonedaEditDtoValidator : AbstractValidator<MonedaEditDto>
{
    public MonedaEditDtoValidator()
    {

        RuleFor(x => x.NomMoneda)
            .NotEmpty()
            .MaximumLength(50)
            .WithName("Nombre de la Moneda");

        RuleFor(x => x.ClaveMoneda)
            .NotEmpty()
            .MaximumLength(10)
            .WithName("Clave Moneda");
    }
}
