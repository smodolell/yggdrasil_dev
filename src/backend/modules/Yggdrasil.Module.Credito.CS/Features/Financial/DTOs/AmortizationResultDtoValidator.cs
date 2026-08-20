using Yggdrasil.Module.Credito.CS.Features.Financial.Factories;
namespace Yggdrasil.Module.Credito.CS.Features.Financial.DTOs;

public static class FinancialErrorCodes
{
    // Errores de Integridad General (1000+)
    public const string EmptyTable = "FIN_1001";
    public const string NotLiquidated = "FIN_1002";
    public const string BalanceDiscontinuity = "FIN_1003";
    public const string RowArithmeticError = "FIN_1004";
    public const string MissingIva = "FIN_1005";
    public const string UnexpectedIva = "FIN_1006";

    // Errores por Método (2000+)
    public const string FrancesInconsistentFee = "FIN_2001";
    public const string GermanInconsistentCapital = "FIN_3001";
    public const string AmericanInconsistentStructure = "FIN_4001";
}

public class AmortizationResultDtoValidator : AbstractValidator<AmortizationResultDto>
{
    public AmortizationResultDtoValidator()
    {
        // --- 1. Reglas de Integridad General ---

        RuleFor(x => x.TablaAmortiza)
            .NotEmpty()
            .WithErrorCode(FinancialErrorCodes.EmptyTable)
            .WithMessage("La tabla de amortización no puede estar vacía.");

        RuleFor(x => x.TablaAmortiza.Last().SaldoFinal)
            .Equal(0)
            .WithErrorCode(FinancialErrorCodes.NotLiquidated)
            .WithMessage("Error Crítico: La tabla no amortiza a cero al finalizar el plazo.");

        RuleFor(x => x.TablaAmortiza)
            .Must(ValidarContinuidadDeSaldos)
            .WithErrorCode(FinancialErrorCodes.BalanceDiscontinuity)
            .WithMessage("Error de Integridad: Existe una ruptura en la continuidad de los saldos.");

        // 2. Validación de Filas con lógica de IVA
        RuleForEach(x => x.TablaAmortiza).Custom((row, context) =>
        {
            var parentDto = context.InstanceToValidate as AmortizationResultDto;
            if (parentDto == null) return;

            // Validación Aritmética Básica
            if (Math.Abs(row.Total - (row.Capital + row.Interes + row.IVA)) > 0.02m)
            {
                context.AddFailure(new FluentValidation.Results.ValidationFailure(
                    $"{context.DisplayName}[{row.NoPago}]",
                    $"Fila {row.NoPago}: La suma de componentes no coincide con el Total.",
                    row.Total)
                { ErrorCode = FinancialErrorCodes.RowArithmeticError });
            }

            // --- Lógica de IVA Condicional ---
            if (parentDto.GeneraIVAInteres)
            {
                if (row.Interes > 0 && row.IVA <= 0)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(
                        $"{context.DisplayName}[{row.NoPago}]",
                        $"Fila {row.NoPago}: Se esperaba IVA por configuración de GeneraIVAInteres.")
                    { ErrorCode = FinancialErrorCodes.MissingIva });
                }
            }
            else
            {
                if (row.IVA > 0)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(
                        $"{context.DisplayName}[{row.NoPago}]",
                        $"Fila {row.NoPago}: Se detectó IVA pero el crédito está marcado como exento.")
                    { ErrorCode = FinancialErrorCodes.UnexpectedIva });
                }
            }
        });

        // --- 3. Reglas Específicas por Método ---

        // FRANCÉS (Cuota Nivelada - Sensible a Gracia)
        When(x => x.Method == AmortizationMethod.French, () =>
        {
            RuleFor(x => x.TablaAmortiza)
                .Must(ValidarCuotaConstanteFrancesa)
                .WithErrorCode(FinancialErrorCodes.FrancesInconsistentFee)
                .WithMessage("Inconsistencia: En el método Francés, la cuota total de los periodos amortizables debe ser constante.");
        });

        // ALEMÁN (Capital Fijo - Sensible a Gracia)
        When(x => x.Method == AmortizationMethod.German, () =>
        {
            RuleFor(x => x.TablaAmortiza)
                .Must(ValidarCapitalConstanteAleman)
                .WithErrorCode(FinancialErrorCodes.GermanInconsistentCapital)
                .WithMessage("Inconsistencia: En el método Alemán, el capital amortizable debe ser el mismo en cada período.");
        });

        // AMERICANO (Bullet)
        When(x => x.Method == AmortizationMethod.American, () =>
        {
            RuleFor(x => x.TablaAmortiza)
                .Must(ValidarEstructuraAmericana)
                .WithErrorCode(FinancialErrorCodes.AmericanInconsistentStructure)
                .WithMessage("Inconsistencia: En el método Americano, no debe haber amortización de capital hasta el final.");
        });
    }

    #region Helpers de Validación

    private static bool ValidarContinuidadDeSaldos(List<AmortizacionRow> tabla)
    {
        for (int i = 1; i < tabla.Count; i++)
        {
            if (Math.Abs(tabla[i].SaldoInicial - tabla[i - 1].SaldoFinal) > 0.01m)
                return false;
        }
        return true;
    }

    private static bool ValidarCuotaConstanteFrancesa(List<AmortizacionRow> tabla)
    {
        if (tabla.Count < 2) return true;

        // 1. Detectar dinámicamente los periodos de gracia (donde el Capital es estrictamente 0.00)
        // Omitimos el último elemento de la búsqueda para evitar falsos positivos en plazos de un solo pago o americanos
        int periodosGracia = tabla.Take(tabla.Count - 1).TakeWhile(r => r.Capital == 0).Count();

        // 2. Extraer el segmento de cuotas que sí deben amortizar capital
        var filasAmortizables = tabla.Skip(periodosGracia).ToList();
        if (filasAmortizables.Count < 2) return true;

        // 3. Nuestra cuota de referencia es la primera cuota amortizable completa
        decimal cuotaReferencia = filasAmortizables[0].Capital + filasAmortizables[0].Interes + filasAmortizables[0].IVA;

        // 4. Validamos que el segmento amortizable (excluyendo el último pago por centavos de ajuste) sea constante
        return filasAmortizables.Take(filasAmortizables.Count - 1).All(r =>
            Math.Abs((r.Capital + r.Interes + r.IVA) - cuotaReferencia) < 1.00m);
    }

    private static bool ValidarCapitalConstanteAleman(List<AmortizacionRow> tabla)
    {
        if (tabla.Count < 2) return true;

        // 1. Detectar dinámicamente los periodos de gracia
        int periodosGracia = tabla.Take(tabla.Count - 1).TakeWhile(r => r.Capital == 0).Count();

        // 2. Extraer el segmento amortizable
        var filasAmortizables = tabla.Skip(periodosGracia).ToList();
        if (filasAmortizables.Count < 2) return true;

        // 3. El capital constante de referencia es el del primer periodo amortizable
        decimal capitalReferencia = filasAmortizables[0].Capital;

        // 4. Validamos constancia exceptuando la última cuota (que absorbe redondeos del saldo final)
        return filasAmortizables.Take(filasAmortizables.Count - 1).All(r =>
            Math.Abs(r.Capital - capitalReferencia) < 0.05m);
    }

    private static bool ValidarEstructuraAmericana(List<AmortizacionRow> tabla)
    {
        if (!tabla.Any()) return false;
        // El capital intermedio debe ser cero (la gracia es natural hasta el vencimiento)
        return tabla.Take(tabla.Count - 1).All(r => r.Capital == 0) && tabla.Last().Capital > 0;
    }

    #endregion
}