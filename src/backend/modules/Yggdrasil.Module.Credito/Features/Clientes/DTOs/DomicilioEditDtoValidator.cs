namespace Yggdrasil.Module.Credito.Features.Clientes.DTOs;

public class DomicilioEditDtoValidator : AbstractValidator<DomicilioEditDto>
{
    public DomicilioEditDtoValidator()
    {
        RuleFor(r => r.TipoDomicilioId)
            .NotNull()
            .GreaterThan(0)
            .WithName("Tipo de Domicilio");

        RuleFor(r => r.Calle)
            .NotEmpty();

        RuleFor(r => r.Numero).NotEmpty();

        RuleFor(r => r.LocalidadId)
            .GreaterThanOrEqualTo(0)
            .WithName("Localidad");

        //RuleFor(r => r.EntreCalle).NotEmpty();
        //RuleFor(r => r.YCalle).NotEmpty();
    }

}
