using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class CreateTasaFijaCommand : ICommand<Result<int>>
{
    public required TasaFijaEditDto Model { get; set; }
}

public class CreateTasaFijaCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<TasaFijaEditDto> validator
) : ICommandHandler<CreateTasaFijaCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<TasaFijaEditDto> _validator = validator;

    public async Task<Result<int>> HandleAsync(CreateTasaFijaCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        var oTasa = new CAT_Tasa { EsVariable = false };
        _context.CAT_Tasa.Add(oTasa);
        _mapper.Map(model, oTasa);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(oTasa.Id);
    }
}
