using Yggdrasil.Common.Constants;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;

public class ConceptoFinanciadoEditDto
{
    public int CargoId { get; set; }
    public int ProductoId { get; set; }
    public int? TipoMovimientoId { get; set; }
    public int TipoCalculoId { get; set; } = AppConstants.CAT_TipoCalculo_MontoFijo;
    public string Concepto { get; set; } = "";
    public decimal Monto { get; set; }
    public decimal Porcentaje { get; set; }
    public bool PermiteEdicion { get; set; }
}

public class ConceptoFinanciadoListItemDto
{
    public int Id { get; set; }
    public string NomTipoMovimiento { get; set; } = "";
    public string NomTipoCalculo { get; set; } = "";
    public string Concepto { get; set; } = "";
    public decimal Monto { get; set; }
    public bool PermiteEdicion { get; set; }
    public bool Activo { get; set; }
}

public class ConceptoFinanciadoEditDtoValidator : AbstractValidator<ConceptoFinanciadoEditDto>
{
    public ConceptoFinanciadoEditDtoValidator()
    {
        RuleFor(r => r.Concepto)
            .NotEmpty()
            .MaximumLength(80);

        RuleFor(r => r.TipoMovimientoId)
            .NotNull();

        RuleFor(r => r.TipoCalculoId)
            .NotNull();

        RuleFor(x => x.Monto)
            .NotNull()
            .GreaterThan(0)
            .When(x => x.TipoCalculoId == AppConstants.CAT_TipoCalculo_MontoFijo);

        RuleFor(x => x.Porcentaje)
            .NotNull()
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .When(x => x.TipoCalculoId == AppConstants.CAT_TipoCalculo_PorcentajeCapital);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ConceptoFinanciadoEditDto>.CreateWithOptions((ConceptoFinanciadoEditDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
