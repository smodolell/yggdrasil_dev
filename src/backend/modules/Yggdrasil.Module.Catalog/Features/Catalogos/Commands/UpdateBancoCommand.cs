using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class UpdateBancoCommand : ICommand<Result>
{
    public int BancoId { get; set; }
    public required BancoEditDto Model { get; set; }
}

internal class UpdateBancoCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<BancoEditDto> validator
) : ICommandHandler<UpdateBancoCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<BancoEditDto> _validator = validator;

    public async Task<Result> HandleAsync(UpdateBancoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }
            var oBanco = await _context.CAT_Banco.SingleOrDefaultAsync(r => r.Id == message.BancoId, cancellationToken);
            if (oBanco == null)
            {
                return Result.NotFound();
            }

            _mapper.Map(model, oBanco);
            _context.CAT_Banco.Update(oBanco);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
