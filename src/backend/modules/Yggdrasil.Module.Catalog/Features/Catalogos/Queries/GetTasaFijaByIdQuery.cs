using Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

namespace Yggdrasil.Module.Catalog.Features.Catalogos.Queries;

public class GetTasaFijaByIdQuery : IQuery<Result<TasaFijaEditDto>>
{
    public int TasaId { get; set; }
}

public class GetTasaFijaByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetTasaFijaByIdQuery, Result<TasaFijaEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<TasaFijaEditDto>> HandleAsync(GetTasaFijaByIdQuery message, CancellationToken cancellationToken = default)
    {
        var oTasa = await _context.CAT_Tasa.SingleOrDefaultAsync(r => r.Id == message.TasaId, cancellationToken);
        if (oTasa == null)
        {
            return Result.NotFound();
        }
        var tasaDto = _mapper.Map<TasaFijaEditDto>(oTasa);
        return Result.Success(tasaDto);
    }
}
