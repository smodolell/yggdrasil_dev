namespace Yggdrasil.Module.Credito.UI.Services.Configuacion.DTOs;

public class ProductoCreateDto
{
    public int? EmpresaId { get; set; }
    public int? MonedaId { get; set; }
    public string ClaveProducto { get; set; } = "";
    public string NomProducto { get; set; } = "";
    public string Posfijo { get; set; } = "";
    public string Prefijo { get; set; } = "";
    public int? TipoMovimientoRentaId { get; set; }
    public int? TipoMovimientoMoraId { get; set; }
    public decimal MoraPorDefecto { get; set; }
    public bool Activo { get; set; } = true;
}

public class ProductoCreateDtoValidator : AbstractValidator<ProductoCreateDto>
{
    public ProductoCreateDtoValidator()
    {
        RuleFor(x => x.EmpresaId)
            .NotNull()
            .WithName("Empresa");

        RuleFor(x => x.MonedaId)
            .NotNull()
            .WithName("Moneda");

        RuleFor(x => x.ClaveProducto)
            .NotEmpty()
            .MaximumLength(5)
            .WithName("Clave del Producto");

        RuleFor(x => x.NomProducto)
            .NotEmpty()
            .MaximumLength(200)
            .WithName("Nombre del Producto");

        RuleFor(x => x.Posfijo)
            .NotEmpty()
            .MaximumLength(8)
            .WithName("Posfijo");

        RuleFor(x => x.Prefijo)
            .NotEmpty()
            .MaximumLength(8)
            .WithName("Prefijo");

        RuleFor(x => x.TipoMovimientoRentaId)
            .NotNull()
            .WithName("Tipo Movimiento Renta");
    }
}
