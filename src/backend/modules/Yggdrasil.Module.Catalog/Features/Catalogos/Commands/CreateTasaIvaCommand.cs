using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Commands;

public class CreateTasaIvaCommand : ICommand<Result<int>>
{
    public required TasaIvaEditDto Model { get; set; }
}

public class CreateTasaIvaCommandHandler(
    IApplicationDbContext context,
    IMapper mapper,
    IValidator<TasaIvaEditDto> validator
) : ICommandHandler<CreateTasaIvaCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<TasaIvaEditDto> _validator = validator;

    public async Task<Result<int>> HandleAsync(CreateTasaIvaCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        var oTasaIva = new CAT_TasaIva();
        _context.CAT_TasaIva.Add(oTasaIva);
        _mapper.Map(model, oTasaIva);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(oTasaIva.Id);
    }
}
