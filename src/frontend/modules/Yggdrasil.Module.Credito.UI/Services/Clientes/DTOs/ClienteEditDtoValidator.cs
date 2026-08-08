namespace Yggdrasil.Module.Credito.UI.Services.Clientes.DTOs;

public class ClienteEditDtoValidator : AbstractValidator<ClienteEditDto>
{

    public ClienteEditDtoValidator()
    {


        RuleFor(p => p.TipoPersonaId)
           .NotNull();

        #region Persona Fisica 

        RuleFor(x => x.CUIT)
            .NotEmpty()
            .MaximumLength(30)
            .WithName("CUIT")
            .When(IsPersonaFisica());

        RuleFor(x => x.Nombre)
            .NotEmpty()
            .MaximumLength(100)
            .WithName("Nombre")
            .When(IsPersonaFisica());

        RuleFor(r => r.Apellido)
            .NotEmpty()
            .MaximumLength(80)
            .WithName("Apellido")
            .When(IsPersonaFisica());



        RuleFor(p => p.FechaNacimiento)
            .NotNull()
            .NotEmpty()
            .Must(EdadEnRango)
            .WithMessage("La edad debe estar entre 18 y 60 años.")
            .When(IsPersonaFisica());



        #endregion

        #region Persona Juridica

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




        RuleFor(c => c.DNI)
            .NotEmpty()
            .WithMessage("El DNI es obligatorio.")
            //.When(c => c.PersonaId == 0)
            //.MustAsync(async (rfc, cancellation) => 
            //{
            //    var  result = !await _clienteValidatorService.ExisteRfc(rfc??"");
            //    return result;
            //}).WithMessage("El RFC ya existe en la base de datos.")
            ;


        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Pone un email. Cara de ura");

    }



    private bool EdadEnRango(DateTime? fechaNacimiento)
    {
        if (fechaNacimiento == null) return false;
        var edad = CalcularEdad(fechaNacimiento ?? DateTime.Now);
        return edad >= 18 && edad <= 60;
    }

    private int CalcularEdad(DateTime fechaNacimiento)
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


    public Func<ClienteEditDto, bool> IsPersonaFisica()
    {
        return p => p.TipoPersonaId == 1;
    }

    public Func<ClienteEditDto, bool> IsPersonaJuridica()
    {
        return p => p.TipoPersonaId == 2;
    }

}
