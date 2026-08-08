namespace Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

public class TasaFijaEditDto
{
    public int TasaId { get; set; }
    public decimal ValorTasa { get; set; }
    public string NomTasa { get; set; } = "";
    public bool Activo { get; set; }
}

public class TasaFijaEditDtoValidator : AbstractValidator<TasaFijaEditDto>
{
    public TasaFijaEditDtoValidator()
    {
        RuleFor(x => x.NomTasa)
            .NotEmpty()
            .MaximumLength(30)
            .WithName("Nombre Tasa");

        RuleFor(x => x.ValorTasa)
            .GreaterThan(0)
            .WithName("Valor Tasa");
    }
}
