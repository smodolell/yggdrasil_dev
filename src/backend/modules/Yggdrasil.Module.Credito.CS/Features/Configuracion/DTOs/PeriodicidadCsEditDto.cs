
namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;


public class PeriodicidadCsEditDto
{
    public string ClavePeriodicidad { get; set; } = "";
    public string NomPeriodicidad { get; set; } = "";
    public short ParamDias { get; set; }
    public short ParamMes { get; set; }
    public short NroPagosAnio { get; set; }
    public short NroPagosMes { get; set; }
    public bool UsaDias { get; set; }
    public bool Activo { get; set; }
}

public class PeriodicidadCsEditDtoValidator : AbstractValidator<PeriodicidadCsEditDto>
{
    public PeriodicidadCsEditDtoValidator()
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

        RuleFor(x => x.NroPagosAnio)
            .GreaterThanOrEqualTo((short)0)
            .WithName("Número de Pagos por Año");

        RuleFor(x => x.NroPagosMes)
            .GreaterThanOrEqualTo((short)0)
            .WithName("Número de Pagos por Mes");
    }
}
