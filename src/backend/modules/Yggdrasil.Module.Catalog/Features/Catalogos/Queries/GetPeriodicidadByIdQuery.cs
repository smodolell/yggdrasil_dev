using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Queries;

public class GetPeriodicidadByIdQuery : IQuery<Result<PeriodicidadEditDto>>
{
    public int PeriodicidadId { get; set; }
}

public class GetPeriodicidadByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetPeriodicidadByIdQuery, Result<PeriodicidadEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<PeriodicidadEditDto>> HandleAsync(GetPeriodicidadByIdQuery message, CancellationToken cancellationToken = default)
    {
        var oPeriodicidad = await _context.CAT_Periodicidad.SingleOrDefaultAsync(r => r.Id == message.PeriodicidadId, cancellationToken);
        if (oPeriodicidad == null)
        {
            return Result.NotFound();
        }
        var periodicidadDto = _mapper.Map<PeriodicidadEditDto>(oPeriodicidad);
        return Result.Success(periodicidadDto);
    }
}
