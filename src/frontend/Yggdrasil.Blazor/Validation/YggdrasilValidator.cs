using FluentValidation;

namespace Yggdrasil.Blazor.Validation;

public abstract class YggdrasilValidator<T> : AbstractValidator<T>
{
    protected YggdrasilValidator()
    {
        // Configuración global: por ejemplo, detenerse en el primer error
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}
