namespace Yggdrasil.Module.Credito.UI.Services.Clientes.DTOs;

public class TelefonoEditDtoValidator : AbstractValidator<TelefonoEditDto>
{
    public TelefonoEditDtoValidator()
    {
        RuleFor(r => r.FechaRegistro);
        RuleFor(r => r.TipoTelefonoId).NotNull().GreaterThan(0);
        RuleFor(r => r.PersonaId).NotNull().GreaterThan(0);
        RuleFor(r => r.Numero).NotEmpty();
        RuleFor(r => r.Extension).NotEmpty();
        RuleFor(r => r.InfoAdicional).NotEmpty();
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<TelefonoEditDto>.CreateWithOptions((TelefonoEditDto)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
