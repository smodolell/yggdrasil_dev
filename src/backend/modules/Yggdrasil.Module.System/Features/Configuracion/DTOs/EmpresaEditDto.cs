using System.ComponentModel.DataAnnotations;

namespace Yggdrasil.Module.System.Features.Configuracion.DTOs;

public class EmpresaEditDto
{
    public int EmpresaId { get; set; }

    [Required]
    [MaxLength(100)]

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