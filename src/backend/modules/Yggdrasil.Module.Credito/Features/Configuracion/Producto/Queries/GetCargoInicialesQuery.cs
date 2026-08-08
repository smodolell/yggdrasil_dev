using Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;
using Yggdrasil.Module.Credito.Features.Configuracion.Producto.Specifications;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Queries;

public class GetCargoInicialesQuery : IQuery<Result<List<CargoInicialListItemDto>>>
{
    public required int ProductoId { get; set; }
}

internal class GetCargoInicialesQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetCargoInicialesQuery, Result<List<CargoInicialListItemDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<CargoInicialListItemDto>>> HandleAsync(GetCargoInicialesQuery message, CancellationToken cancellationToken = default)
    {
        var spec = new CargoInicialSpec(message.ProductoId);
        var data = await _context.FI_Cargo
            .Include(c => c.FI_TipoCalculo)
            .Include(c => c.FI_TipoMovimiento)
            .Include(c => c.FI_FormaPago)
            .WithSpecification(spec)
            .ToListAsync(cancellationToken);
        var result = _mapper.Map<List<CargoInicialListItemDto>>(data);

        return Result.Success(result);
    }
}
