namespace Yggdrasil.Module.Credito.Features.Clientes.DTOs;

public class DomicilioEditDto
{
    public int? DomicilioId { get; set; }
    public DateTime? FechaRegistro { get; set; }
    public int PersonaId { get; set; }
    public long LocalidadId { get; set; }

    public string Calle { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Piso { get; set; } = "";
    public string EntreCalles { get; set; } = string.Empty;
    public string YCalle { get; set; } = string.Empty;

    public int? TipoDomicilioId { get; set; }


}
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
