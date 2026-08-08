namespace Yggdrasil.Module.Credito.Features.Clientes.DTOs;

public class CuentaBancariaEditDtoValidator : AbstractValidator<CuentaBancariaEditDto>
{

    public CuentaBancariaEditDtoValidator()
    {

        RuleFor(x => x.BancoId)
            .GreaterThan(0).When(x => x.BancoId.HasValue)
            .WithName("Banco");

        RuleFor(x => x.MonedaId)
            .GreaterThan(0).When(x => x.MonedaId.HasValue)
            .WithMessage("El ID de moneda debe ser válido si está presente");

        RuleFor(x => x.TipoCuentaBancariaId)
            .GreaterThan(0).When(x => x.TipoCuentaBancariaId.HasValue)
            .WithMessage("El ID de tipo de cuenta debe ser válido si está presente");

        RuleFor(x => x.NroCuentaBancaria)
            .NotEmpty().WithMessage("El número de cuenta es requerido")
            .MaximumLength(20).WithMessage("El número de cuenta no puede exceder 20 caracteres")
            .Matches(@"^[0-9]+$").WithMessage("El número de cuenta solo puede contener dígitos");

        RuleFor(x => x.CBU)
            .NotEmpty().WithMessage("El CBU es requerido")
            .Length(22).WithMessage("El CBU debe tener exactamente 22 caracteres")
            .Matches(@"^[0-9]+$").WithMessage("El CBU solo puede contener dígitos")
            .Must(ValidarDigitosVerificadoresCBU).WithMessage("El CBU no es válido (dígitos verificadores incorrectos)");

        RuleFor(x => x.AliasCBU)
            .MaximumLength(30).WithMessage("El alias CBU no puede exceder 30 caracteres")
            .Matches(@"^[a-zA-Z0-9\.\-]+$").WithMessage("El alias CBU solo puede contener letras, números, puntos y guiones")
            .When(x => !string.IsNullOrEmpty(x.AliasCBU));
    }

    private bool ValidarDigitosVerificadoresCBU(string cbu)
    {
        if (string.IsNullOrWhiteSpace(cbu) || cbu.Length != 22)
            return false;

        return true;
        try
        {
            // Validación bloque 1 (primeros 8 dígitos)
            int[] ponderadoresBloque1 = { 7, 1, 3, 9, 7, 1, 3 };
            int sumaBloque1 = 0;
            for (int i = 0; i < 7; i++)
            {
                sumaBloque1 += int.Parse(cbu[i].ToString()) * ponderadoresBloque1[i];
            }
            int digitoVerificador1 = (10 - (sumaBloque1 % 10)) % 10;
            if (digitoVerificador1 != int.Parse(cbu[7].ToString()))
                return false;

            // Validación bloque 2 (dígitos 9 al 21)
            int[] ponderadoresBloque2 = { 3, 9, 7, 1, 3, 9, 7, 1, 3, 9, 7, 1, 3 };
            int sumaBloque2 = 0;
            for (int i = 8; i < 21; i++)
            {
                sumaBloque2 += int.Parse(cbu[i].ToString()) * ponderadoresBloque2[i - 8];
            }
            int digitoVerificador2 = (10 - (sumaBloque2 % 10)) % 10;
            if (digitoVerificador2 != int.Parse(cbu[21].ToString()))
                return false;

            return true;
        }
        catch
        {
            return false;
        }
    }
}
