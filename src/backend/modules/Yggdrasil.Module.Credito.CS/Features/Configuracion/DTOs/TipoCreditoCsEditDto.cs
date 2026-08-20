namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;

public class TipoCreditoCsEditDto
{
    public string ClaveTipoCredito { get; set; } = "";
    public string NomTipoCredito { get; set; } = "";
    public int? TipoMovimientoRentaId { get; set; }
    public bool Activo { get; set; } = true;
}

public class TipoCreditoCsEditDtoValidator : AbstractValidator<TipoCreditoCsEditDto>
{
    public TipoCreditoCsEditDtoValidator()
    {
        RuleFor(x => x.ClaveTipoCredito)
            .NotEmpty()
            .MaximumLength(10)
            .WithName("Clave Tipo de Crédito");

        RuleFor(x => x.NomTipoCredito)
            .NotEmpty()
            .MaximumLength(80)
            .WithName("Nombre Tipo de Crédito");

        RuleFor(x => x.TipoMovimientoRentaId)
            .NotNull()
            .WithName("Tipo de Movimiento de Renta");
    }
}
