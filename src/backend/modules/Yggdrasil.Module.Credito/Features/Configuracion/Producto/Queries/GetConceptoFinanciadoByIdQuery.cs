using Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Queries;

public class GetConceptoFinanciadoByIdQuery : IQuery<Result<ConceptoFinanciadoEditDto>>
{
    public required int CargoId { get; set; }
}

internal class GetConceptoFinanciadoByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetConceptoFinanciadoByIdQuery, Result<ConceptoFinanciadoEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<ConceptoFinanciadoEditDto>> HandleAsync(GetConceptoFinanciadoByIdQuery message, CancellationToken cancellationToken = default)
    {
        var cargo = await _context.FI_Cargo.SingleOrDefaultAsync(r => r.Id == message.CargoId, cancellationToken);
        if (cargo == null) return Result.Error($"[NO_EXISTE][{nameof(FI_Cargo)}]");
        var result = _mapper.Map<ConceptoFinanciadoEditDto>(cargo);
        return Result.Success(result);
    }
}
