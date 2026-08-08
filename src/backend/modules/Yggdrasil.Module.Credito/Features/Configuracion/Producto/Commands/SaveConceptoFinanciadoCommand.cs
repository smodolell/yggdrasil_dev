using Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Commands;

public record SaveConceptoFinanciadoCommand(ConceptoFinanciadoEditDto Model) : ICommand<Result<int>>;

internal class SaveConceptoFinanciadoCommandHandler(
    IApplicationDbContext context,
    IValidator<ConceptoFinanciadoEditDto> validator,
    IMapper mapper
) : ICommandHandler<SaveConceptoFinanciadoCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IValidator<ConceptoFinanciadoEditDto> _validator = validator;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<int>> HandleAsync(SaveConceptoFinanciadoCommand message, CancellationToken cancellationToken = default)
    {
        var model = message.Model;
        var validationResult = await _validator.ValidateAsync(model, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        var cargo = await _context.FI_Cargo.SingleOrDefaultAsync(r => r.Id == model.CargoId, cancellationToken);
        if (cargo == null)
        {
            cargo = new FI_Cargo
            {
                ProductoId = model.ProductoId,
                EsConceptoFinanciado = true,
            };
            _context.FI_Cargo.Add(cargo);
        }
        _mapper.Map(model, cargo);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(cargo.Id);
    }
}
