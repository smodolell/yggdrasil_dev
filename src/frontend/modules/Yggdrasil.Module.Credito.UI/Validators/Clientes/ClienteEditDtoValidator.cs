using Yggdrasil.ApiClient.Contracts;

namespace Yggdrasil.Module.Credito.UI.Validators.Clientes;

public class ClienteEditDtoValidator : AbstractValidator<ClienteEditDto>
{

    public ClienteEditDtoValidator()
    {


        RuleFor(p => p.TipoPersonaId)
           .NotNull();


        RuleFor(x => x.Rfc)
            .NotEmpty()
            .MaximumLength(30)
            .WithName("RFC");

        #region Persona Fisica 

        RuleFor(x => x.Curp)
            .NotEmpty()
            .MaximumLength(30)
            .WithName("CURP")
            .When(IsPersonaFisica());

        RuleFor(x => x.PrimerNombre)
            .NotEmpty()
            .MaximumLength(100)
            .WithName("Primer Nombre")
            .When(IsPersonaFisica());

        RuleFor(x => x.SegundoNombre)
           .NotEmpty()
           .MaximumLength(100)
           .WithName("Segundo Nombre")
           .When(IsPersonaFisica());

        RuleFor(r => r.ApellidoPaterno)
            .NotEmpty()
            .MaximumLength(80)
            .WithName("Apellido Paterno")
            .When(IsPersonaFisica());

        RuleFor(r => r.ApellidoMaterno)
            .NotEmpty()
            .MaximumLength(80)
            .WithName("Apellido Materno")
            .When(IsPersonaFisica());



        RuleFor(p => p.FechaNacimiento)
            .NotNull()
            .NotEmpty()
            .Must(EdadEnRango)
            .WithMessage("La edad debe estar entre 18 y 60 años.")
            .When(IsPersonaFisica());



        #endregion

        #region Persona Moral

        RuleFor(r => r.RazonSocial)
           .NotEmpty()
           .MaximumLength(150)
           .WithName("Razón Social")
           .When(IsPersonaJuridica());

        RuleFor(p => p.FechaConstitucion)
            .NotNull()
            .NotEmpty()
            .When(IsPersonaJuridica());

        #endregion





        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Pone un email. Cara de ura");

    }



    private static bool EdadEnRango(DateTime? fechaNacimiento)
    {
        if (fechaNacimiento == null) return false;
        var edad = CalcularEdad(fechaNacimiento ?? DateTime.Now);
        return edad >= 18 && edad <= 60;
    }

    private static int CalcularEdad(DateTime fechaNacimiento)
    {
        var today = DateTime.Today;
        var edad = today.Year - fechaNacimiento.Year;

        // Restar un año si el cumpleaños aún no ha ocurrido este año
        if (fechaNacimiento.Date > today.AddYears(-edad))
        {
            edad--;
        }

        return edad;
    }


    public static Func<ClienteEditDto, bool> IsPersonaFisica()
    {
        return p => p.TipoPersonaId == 1;
    }

    public static Func<ClienteEditDto, bool> IsPersonaJuridica()
    {
        return p => p.TipoPersonaId == 2;
    }

}
