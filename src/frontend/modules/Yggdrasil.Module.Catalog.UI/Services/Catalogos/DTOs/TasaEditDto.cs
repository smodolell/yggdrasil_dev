namespace Yggdrasil.Module.Catalog.UI.Services.Catalogos.DTOs;

public class TasaEditDto
{
    public int TasaId { get; set; }
    public decimal ValorTasa { get; set; }
    public string NomTasa { get; set; } = "";
    public bool Activo { get; set; }
}

public class TasaEditDtoValidator : AbstractValidator<TasaEditDto>
{
    public TasaEditDtoValidator()
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
