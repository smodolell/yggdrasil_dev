namespace Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

public class PeriodicidadEditDto
{
    public int PeriodicidadId { get; set; }
    public string ClavePeriodicidad { get; set; } = "";
    public string NomPeriodicidad { get; set; } = "";
    public short ParamDias { get; set; }
    public short ParamMes { get; set; }
    public short NroPagosAnio { get; set; }
    public short NroPagosMes { get; set; }
    public bool UsaDias { get; set; }
    public bool Activo { get; set; }
}

public class PeriodicidadEditDtoValidator : AbstractValidator<PeriodicidadEditDto>
{
    public PeriodicidadEditDtoValidator()
    {

        RuleFor(x => x.ClavePeriodicidad)
            .NotEmpty()
            .MaximumLength(10)
            .WithName("Clave Periodicidad");

        RuleFor(x => x.NomPeriodicidad)
            .NotEmpty()
            .MaximumLength(60)
            .WithName("Nombre Periodicidad");

        RuleFor(x => x.ParamDias)
            .GreaterThanOrEqualTo((short)0)
            .WithName("Parámetro Días");

        RuleFor(x => x.ParamMes)
            .GreaterThanOrEqualTo((short)0)
            .WithName("Parámetro Mes");
    }
}
