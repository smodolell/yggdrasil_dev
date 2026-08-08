namespace Yggdrasil.Module.Catalog.UI.Services.Catalogos.DTOs;

public class TasaVariableDto
{
    public string NomTasa { get; set; } = "";
    public bool Activo { get; set; }
}

public class TasaVariableDtoValidator : AbstractValidator<TasaVariableDto>
{
    public TasaVariableDtoValidator()
    {
        RuleFor(x => x.NomTasa)
            .NotEmpty()
            .MaximumLength(100)
            .WithName("Nombre Tasa Variable");

    }
}
