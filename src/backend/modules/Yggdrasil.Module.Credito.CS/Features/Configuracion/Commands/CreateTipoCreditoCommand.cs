using Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.Commands;

public class CreateTipoCreditoCommand : ICommand<Result<int>>
{
    public required TipoCreditoCsEditDto Model { get; set; }
}

public class CreateTipoCreditoCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<TipoCreditoCsEditDto> validator
) : ICommandHandler<CreateTipoCreditoCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<TipoCreditoCsEditDto> _validator = validator;

    public async Task<Result<int>> HandleAsync(CreateTipoCreditoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            var oTipoCredito = _mapper.Map<CS_TipoCredito>(model);
            _context.CS_TipoCredito.Add(oTipoCredito);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(oTipoCredito.Id);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
