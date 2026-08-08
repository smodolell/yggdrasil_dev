namespace Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

public class TasaIvaEditDto
{
    public int TasaIvaId { get; set; }
    public decimal ValorTasa { get; set; }
    public string NomTasaIva { get; set; } = "";
    public bool Activo { get; set; }
}

public class TasaIvaEditDtoValidator : AbstractValidator<TasaIvaEditDto>
{
    public TasaIvaEditDtoValidator()
    {
        RuleFor(x => x.NomTasaIva)
            .NotEmpty()
            .MaximumLength(30)
            .WithName("Nombre Tasa IVA");

        RuleFor(x => x.ValorTasa)
            .GreaterThanOrEqualTo(0)
            .WithName("Valor Tasa");
    }
}
