namespace Yggdrasil.Module.Credito.UI.Services.Configuacion.DTOs;

public class TipoMovimientoEditDtoValidator : AbstractValidator<TipoMovimientoEditDto>
{
    public TipoMovimientoEditDtoValidator()
    {

        RuleFor(x => x.Clave)
            .NotEmpty()
            .WithMessage("La clave es requerida.")
            .MaximumLength(6)
            .WithMessage("La clave no puede tener más de 6 caracteres.");

        RuleFor(x => x.NomTipoMovimiento)
            .NotEmpty().WithMessage("El nombre del tipo de movimiento es requerido.")
            .MaximumLength(60).WithMessage("El nombre no puede tener más de 60 caracteres.");

        RuleFor(x => x.GeneraIvaCapital)
            .NotNull()
            .WithMessage("El campo 'Genera IVA Capital' es requerido.");

        RuleFor(x => x.GeneraIvaInteres)
            .NotNull()
            .WithMessage("El campo 'Genera IVA Interes' es requerido.");

        RuleFor(x => x.EsCargoInicial)
            .NotNull().WithMessage("El campo 'Cargo Inicial' es requerido.");

        RuleFor(x => x.EsConceptoFinanciado)
            .NotNull().WithMessage("El campo 'Concepto Financiado' es requerido.");

        RuleFor(x => x.Activo)
            .NotNull().WithMessage("El campo 'Activo' es requerido.");
    }
}