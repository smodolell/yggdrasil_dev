using Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.Queries;

public class GetCargoInicialByIdQuery : IQuery<Result<CargoInicialEditDto>>
{
    public required int CargoId { get; set; }
}

internal class GetCargoInicialEditQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetCargoInicialByIdQuery, Result<CargoInicialEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CargoInicialEditDto>> HandleAsync(GetCargoInicialByIdQuery message, CancellationToken cancellationToken = default)
    {
        var cargo = await _context.FI_Cargo.SingleOrDefaultAsync(r => r.Id == message.CargoId, cancellationToken);
        if (cargo == null) return Result.Error($"[NO_EXISTE][{nameof(FI_Cargo)}]");
        var result = _mapper.Map<CargoInicialEditDto>(cargo);
        return Result.Success(result);
    }
}
