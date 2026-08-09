namespace Yggdrasil.Module.Catalog.UI.Services.Catalogos.DTOs;

public class BancoEditDto
{
    //public int BancoId { get; set; }
    public string NomBanco { get; set; } = "";
    public string CodigoBCRA { get; set; } = "";
    public string CBUPrefix { get; set; } = "";
}

public class BancoEditDtoValidator : AbstractValidator<BancoEditDto>
{
    public BancoEditDtoValidator()
    {
        RuleFor(x => x.NomBanco)
            .NotEmpty()
            .MaximumLength(30)
            .WithName("Nombre del Banco");

        RuleFor(x => x.CodigoBCRA)
            .NotEmpty()
            .MaximumLength(3)
            .WithName("Código BCRA");

        RuleFor(x => x.CBUPrefix)
            .NotEmpty()
            .MaximumLength(3)
            .WithName("CBU Prefix");
    }
}
