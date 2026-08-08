using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Queries;

public class GetPlazoByIdQuery : IQuery<Result<PlazoEditDto>>
{
    public int PlazoId { get; set; }
}

public class GetPlazoByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetPlazoByIdQuery, Result<PlazoEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<PlazoEditDto>> HandleAsync(GetPlazoByIdQuery message, CancellationToken cancellationToken = default)
    {
        var oPlazo = await _context.CAT_Plazo.SingleOrDefaultAsync(r => r.Id == message.PlazoId, cancellationToken);
        if (oPlazo == null)
        {
            return Result.NotFound();
        }
        var plazoDto = _mapper.Map<PlazoEditDto>(oPlazo);
        return Result.Success(plazoDto);
    }
}
