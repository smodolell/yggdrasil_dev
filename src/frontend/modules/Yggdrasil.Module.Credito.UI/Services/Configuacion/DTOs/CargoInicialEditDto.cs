namespace Yggdrasil.Module.Credito.UI.Services.Configuacion.DTOs;

public class CargoInicialEditDto
{
    public int CargoId { get; set; }
    public int ProductoId { get; set; }
    public int? TipoMovimientoId { get; set; }
    public int? TipoCalculoId { get; set; }
    public int? FormaPagoId { get; set; }
    public string Concepto { get; set; } = "";
    public decimal Monto { get; set; }
    public decimal Porcentaje { get; set; }
    public int EquivaleNroPeriodos { get; set; }
    public bool PermiteEdicion { get; set; }
}

public class CargoInicialListItemDto
{
    public int Id { get; set; }
    public string NomTipoMovimiento { get; set; } = "";
    public string NomTipoCalculo { get; set; } = "";
    public string NomFormaPago { get; set; } = "";
    public string Concepto { get; set; } = "";
    public decimal Monto { get; set; }
    public decimal Porcentaje { get; set; }
    public bool PermiteEdicion { get; set; }
    public bool Activo { get; set; }
}

public class CargoInicialEditDtoValidator : AbstractValidator<CargoInicialEditDto>
{
    public CargoInicialEditDtoValidator()
    {
        RuleFor(r => r.Concepto)
            .NotEmpty()
            .MaximumLength(80);

        RuleFor(r => r.TipoMovimientoId)
            .NotNull();

        RuleFor(r => r.TipoCalculoId)
            .NotNull();

        RuleFor(r => r.FormaPagoId)
            .NotNull();

        RuleFor(x => x.Monto)
            .NotNull()
            .GreaterThan(0)
            .When(x => x.TipoCalculoId == 1);

        RuleFor(x => x.Porcentaje)
            .NotNull()
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .When(x => x.TipoCalculoId == 2);

        RuleFor(x => x.EquivaleNroPeriodos)
            .NotNull()
            .GreaterThan(0)
            .When(x => x.TipoCalculoId == 3);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CargoInicialEditDto>.CreateWithOptions((CargoInicialEditDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
