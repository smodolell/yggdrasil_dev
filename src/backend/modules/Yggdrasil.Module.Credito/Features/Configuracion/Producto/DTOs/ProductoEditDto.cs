using System.ComponentModel.DataAnnotations;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;

public class ProductoEditDto
{
    public int ProductoId { get; set; }
    public string ClaveProducto { get; set; } = "";

    [Required]
    [MaxLength(60)]
    public string NomProducto { get; set; } = "";

    [Required]
    public int TipoCotizadorId { get; set; }

    [Required]
    [MaxLength(8)]
    public string Posfijo { get; set; } = "";

    [Required]
    [MaxLength(8)]
    public string Prefijo { get; set; } = "";

    [Required]
    public int Consecutivo { get; set; }

    public int? TipoMovimientoRentaId { get; set; }

    [Display(Name = "Tipo Movimiento Mora")]
    public int? TipoMovimientoMoraId { get; set; }

    [Required]
    [Display(Name = "Valor Mora Por Defecto")]
    public decimal MoraPorDefecto { get; set; }

    [Required]
    [Display(Name = "Renta Anticipada Máxima %")]
    [Range(0, 100)]
    public decimal RangoEnganche { get; set; }

    [Required]
    [Display(Name = "Paso Enganche")]
    [Range(0, 100)]
    public decimal PasoEnganche { get; set; }

    public int? TipoMovimientoEngancheId { get; set; }
    public int? EmpresaId { get; set; }
    public bool Activo { get; set; }
    public bool PermiteEnganche { get; set; }
    public bool PermiteBallonPayment { get; set; }
    public int? TipoMovimientoBallonPaymentId { get; set; }
    public bool PermitePagosEspeciales { get; set; }
    public decimal PorcentajeMaximoPagoEspecial { get; set; }
    public bool PermiteGraciaCapital { get; set; }
    public int PeriodosGraciaCapital { get; set; }
    public bool PermiteGraciaInteres { get; set; }
    public int PeriodosGraciaInteres { get; set; }
    public bool PermiteTADesdeArchivo { get; set; }
}

public class ProductoEditDtoValidator : AbstractValidator<ProductoEditDto>
{
    public ProductoEditDtoValidator()
    {
        RuleFor(x => x.ClaveProducto)
            .NotEmpty()
            .MaximumLength(10);

        RuleFor(x => x.EmpresaId)
            .NotNull();

        RuleFor(x => x.TipoMovimientoRentaId)
            .NotNull();

        RuleFor(x => x.TipoMovimientoEngancheId)
            .NotNull()
            .When(x => x.PermiteEnganche);

        RuleFor(x => x.TipoMovimientoBallonPaymentId)
            .NotNull()
            .When(x => x.PermiteBallonPayment);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ProductoEditDto>.CreateWithOptions((ProductoEditDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
