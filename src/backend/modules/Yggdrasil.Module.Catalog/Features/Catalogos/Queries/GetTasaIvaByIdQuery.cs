using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Queries;

public class GetTasaIvaByIdQuery : IQuery<Result<TasaIvaEditDto>>
{
    public int TasaIvaId { get; set; }
}

public class GetTasaIvaByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetTasaIvaByIdQuery, Result<TasaIvaEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<TasaIvaEditDto>> HandleAsync(GetTasaIvaByIdQuery message, CancellationToken cancellationToken = default)
    {
        var oTasaIva = await _context.CAT_TasaIva.SingleOrDefaultAsync(r => r.Id == message.TasaIvaId, cancellationToken);
        if (oTasaIva == null)
        {
            return Result.NotFound();
        }
        var tasaIvaDto = _mapper.Map<TasaIvaEditDto>(oTasaIva);
        return Result.Success(tasaIvaDto);
    }
}
