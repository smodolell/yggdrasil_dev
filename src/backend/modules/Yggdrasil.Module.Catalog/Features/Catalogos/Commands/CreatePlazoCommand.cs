using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class CreatePlazoCommand : ICommand<Result<int>>
{
    public required PlazoEditDto Model { get; set; }
}

public class CreatePlazoCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<PlazoEditDto> validator
) : ICommandHandler<CreatePlazoCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<PlazoEditDto> _validator = validator;

    public async Task<Result<int>> HandleAsync(CreatePlazoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        var oPlazo = new CAT_Plazo();
        _context.CAT_Plazo.Add(oPlazo);
        _mapper.Map(model, oPlazo);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(oPlazo.Id);
    }
}
