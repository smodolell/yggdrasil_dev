using FluentValidation;

namespace Yggdrasil.Module.System.UI.Services.System.DTOs;

public class EmpresaEditDto
{
    public int EmpresaId { get; set; }
    public string NomEmpresa { get; set; } = "";
}

public class EmpresaEditDtoValidator : AbstractValidator<EmpresaEditDto>
{
    public EmpresaEditDtoValidator()
    {
        RuleFor(x => x.NomEmpresa)
            .NotEmpty().WithMessage("El nombre de la empresa es requerido.")
            .MaximumLength(100).WithMessage("El nombre de la empresa no puede exceder los 100 caracteres.");
    }
}