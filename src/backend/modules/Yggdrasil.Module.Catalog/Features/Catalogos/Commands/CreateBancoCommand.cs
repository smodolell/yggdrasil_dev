using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class CreateBancoCommand : ICommand<Result<int>>
{
    public required BancoEditDto Model { get; set; }
}

public class CreateBancoCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<BancoEditDto> validator
) : ICommandHandler<CreateBancoCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<BancoEditDto> _validator = validator;

    public async Task<Result<int>> HandleAsync(CreateBancoCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var model = message.Model;
            var validationResult = await _validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }
            var oBanco = new CAT_Banco();
            _context.CAT_Banco.Add(oBanco);
            _mapper.Map(model, oBanco);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(oBanco.Id);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
