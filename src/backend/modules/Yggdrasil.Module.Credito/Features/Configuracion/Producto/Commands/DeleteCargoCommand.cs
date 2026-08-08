namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Commands;

public class DeleteCargoCommand : ICommand<Result>
{
    public required int CargoId { get; set; }
}

internal class DeleteCargoCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteCargoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteCargoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var cargo = await _context.FI_Cargo.SingleOrDefaultAsync(r => r.Id == message.CargoId, cancellationToken);
            if (cargo == null)
            {
                return Result.NotFound("No se encontró el Cargo.");
            }
            _context.FI_Cargo.Remove(cargo);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
