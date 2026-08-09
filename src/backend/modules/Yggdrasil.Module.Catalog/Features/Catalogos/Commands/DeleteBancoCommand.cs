namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class DeleteBancoCommand : ICommand<Result>
{
    public required int BancoId { get; set; }
}

public class DeleteBancoCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteBancoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteBancoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var oBanco = await _context.CAT_Banco.SingleOrDefaultAsync(r => r.Id == message.BancoId, cancellationToken);
            if (oBanco == null)
            {
                return Result.NotFound("No se encontró el banco.");
            }
            _context.CAT_Banco.Remove(oBanco);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
