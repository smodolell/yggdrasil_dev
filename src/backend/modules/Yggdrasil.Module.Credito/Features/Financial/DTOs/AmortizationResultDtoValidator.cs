using FluentValidation;
using Yggdrasil.Module.Credito.Features.Financial.Factories;

namespace Yggdrasil.Module.Credito.Features.Financial.DTOs;

public static class FinancialErrorCodes
{
    // Errores de Integridad General (1000+)
    public const string EmptyTable = "FIN_1001";              // Tabla vacía
    public const string NotLiquidated = "FIN_1002";           // No termina en cero
    public const string BalanceDiscontinuity = "FIN_1003";    // Salto en saldos insolutos
    public const string RowArithmeticError = "FIN_1004";      // Suma de componentes incorrecta
    public const string MissingIva = "FIN_1005";              // Interés sin IVA detectado
    public const string UnexpectedIva = "FIN_1006";           // Interés sin IVA detectado

    // Errores por Método (2000+)
    public const string FrancesInconsistentFee = "FIN_2001";  // Cuota no nivelada
    public const string GermanInconsistentCapital = "FIN_3001"; // Capital no constante
    public const string AmericanInconsistentStructure = "FIN_4001"; // Capital antes del vencimiento
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
            // Obtenemos el DTO principal (el padre) desde el RootContext
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
                // Si el DTO dice que SI genera, pero la fila tiene Interés y NO tiene IVA
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
                // Si el DTO dice que NO genera, pero la fila trae valores en IVA
                if (row.IVA > 0)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(
                        $"{context.DisplayName}[{row.NoPago}]",
                        $"Fila {row.NoPago}: Se detectó IVA pero el crédito está marcado como exento.")
                    { ErrorCode = FinancialErrorCodes.UnexpectedIva });
                }
            }
        });

        // --- 2. Reglas Específicas por Método ---

        // FRANCÉS (Cuota Nivelada)
        When(x => x.Method == AmortizationMethod.French, () =>
        {
            RuleFor(x => x.TablaAmortiza)
                .Must(ValidarCuotaConstanteFrancesa)
                .WithErrorCode(FinancialErrorCodes.FrancesInconsistentFee)
                .WithMessage("Inconsistencia: En el método Francés, la cuota total debe ser constante.");
        });

        // ALEMÁN (Capital Fijo)
        When(x => x.Method == AmortizationMethod.German, () =>
        {
            RuleFor(x => x.TablaAmortiza)
                .Must(ValidarCapitalConstanteAleman)
                .WithErrorCode(FinancialErrorCodes.GermanInconsistentCapital)
                .WithMessage("Inconsistencia: En el método Alemán, el capital debe ser el mismo cada mes.");
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
        // Referencia: Capital + Interes + IVA de la segunda fila
        decimal cuotaReferencia = tabla[1].Capital + tabla[1].Interes + tabla[1].IVA;

        return tabla.Skip(1).Take(tabla.Count - 2).All(r =>
            Math.Abs((r.Capital + r.Interes + r.IVA) - cuotaReferencia) < 1.00m);
    }

    private static bool ValidarCapitalConstanteAleman(List<AmortizacionRow> tabla)
    {
        if (tabla.Count < 2) return true;
        decimal capitalReferencia = tabla[0].Capital;
        return tabla.Take(tabla.Count - 1).All(r => Math.Abs(r.Capital - capitalReferencia) < 0.05m);
    }

    private static bool ValidarEstructuraAmericana(List<AmortizacionRow> tabla)
    {
        if (!tabla.Any()) return false;
        // Capital intermedio debe ser cero
        return tabla.Take(tabla.Count - 1).All(r => r.Capital == 0) && tabla.Last().Capital > 0;
    }

    #endregion
}