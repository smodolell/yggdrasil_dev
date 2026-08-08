using Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;
using Yggdrasil.Module.Credito.Features.Configuracion.Producto.Specifications;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Queries;

public class GetConceptoFinanciadosQuery : IQuery<Result<List<ConceptoFinanciadoListItemDto>>>
{
    public required int ProductoId { get; set; }
}

internal class GetConceptoFinanciadosQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetConceptoFinanciadosQuery, Result<List<ConceptoFinanciadoListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<ConceptoFinanciadoListItemDto>>> HandleAsync(GetConceptoFinanciadosQuery message, CancellationToken cancellationToken = default)
    {
        var spec = new ConceptoFinanciadoSpec(message.ProductoId);
        var data = await _context.FI_Cargo
            .WithSpecification(spec)
            .ToListAsync(cancellationToken);
        var result = _mapper.Map<List<ConceptoFinanciadoListItemDto>>(data);
        return Result.Success(result);
    }
}
