using Yggdrasil.Module.System.Features.Configuracion.DTOs;

namespace Yggdrasil.Module.System.Features.Configuracion.Commands;

public class CreateEmpresaCommand : ICommand<Result<int>>
{
    public required EmpresaEditDto Model { get; set; }
}
public class CreateEmpresaCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<EmpresaEditDto> validator
) : ICommandHandler<CreateEmpresaCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<EmpresaEditDto> _validator = validator;

    public async Task<Result<int>> HandleAsync(CreateEmpresaCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        var oEmpresa = new CAT_Empresa();
        _context.CAT_Empresa.Add(oEmpresa);
        _mapper.Map(model, oEmpresa);
        await _context.SaveChangesAsync();

        return Result.Success(oEmpresa.Id);
    }


}