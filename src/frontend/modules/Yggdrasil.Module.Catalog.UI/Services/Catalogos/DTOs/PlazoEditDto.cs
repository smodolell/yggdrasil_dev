namespace Yggdrasil.Module.Catalog.UI.Services.Catalogos.DTOs;

public class PlazoEditDto
{
    public int PlazoId { get; set; }
    public int ValorPlazo { get; set; }
    public bool Activo { get; set; }
}

public class PlazoEditDtoValidator : AbstractValidator<PlazoEditDto>
{
    public PlazoEditDtoValidator()
    {
        RuleFor(x => x.ValorPlazo)
            .GreaterThan(0)
            .WithName("Valor Plazo");
    }
}
