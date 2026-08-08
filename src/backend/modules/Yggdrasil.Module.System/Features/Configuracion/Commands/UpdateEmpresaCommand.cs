using Yggdrasil.Module.System.Features.Configuracion.DTOs;

namespace Yggdrasil.Module.System.Features.Configuracion.Commands;

public class UpdateEmpresaCommand : ICommand<Result>
{
    public int EmpresaId { get; set; }
    public required EmpresaEditDto Model { get; set; }
}

internal class UpdateEmpresaCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<EmpresaEditDto> validator
) : ICommandHandler<UpdateEmpresaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<EmpresaEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdateEmpresaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }
            var oEmpresa = await _context.CAT_Empresa.SingleOrDefaultAsync(r => r.Id == message.EmpresaId, cancellationToken);
            if (oEmpresa == null)
            {
                return Result.NotFound();
            }

            _mapper.Map(model, oEmpresa);
            _context.CAT_Empresa.Update(oEmpresa);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}