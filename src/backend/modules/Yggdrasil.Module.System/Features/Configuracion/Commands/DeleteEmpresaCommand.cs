namespace Yggdrasil.Module.System.Features.Configuracion.Commands;

public class DeleteEmpresaCommand : ICommand<Result>
{
    public required int EmpresaId { get; set; }
}


public class DeleteEmpresaCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteEmpresaCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteEmpresaCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oEmpresa = await _context.CAT_Empresa.SingleOrDefaultAsync(r => r.Id == message.EmpresaId, cancellationToken);
            if (oEmpresa == null)
            {
                return Result.NotFound("No se encontró la empresa.");
            }
            _context.CAT_Empresa.Remove(oEmpresa);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}