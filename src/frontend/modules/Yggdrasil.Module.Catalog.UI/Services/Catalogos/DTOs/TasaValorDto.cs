namespace Yggdrasil.Module.Catalog.UI.Services.Catalogos.DTOs;

public class TasaValorDto
{
    public decimal ValorTasa { get; set; }
    public DateTime FecValorTasa { get; set; }
}

public class TasaValorDtoValidator : AbstractValidator<TasaValorDto>
{
    public TasaValorDtoValidator()
    {
        RuleFor(x => x.ValorTasa)
            .GreaterThanOrEqualTo(0)
            .WithName("Valor Tasa");

        RuleFor(x => x.FecValorTasa)
            .NotEmpty()
            .WithName("Fecha Valor");
    }
}
