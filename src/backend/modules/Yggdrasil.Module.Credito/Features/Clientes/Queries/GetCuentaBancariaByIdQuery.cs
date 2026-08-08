using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Queries;

public record GetCuentaBancariaByIdQuery(int CuentaBancariaId) : IQuery<Result<CuentaBancariaEditDto>>;

internal class GetCuentaBancariaByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetCuentaBancariaByIdQuery, Result<CuentaBancariaEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CuentaBancariaEditDto>> HandleAsync(GetCuentaBancariaByIdQuery message, CancellationToken cancellationToken = default)
    {
        var cuentaBancaria = await _context.FI_PersonaCuentaBancaria
            .SingleOrDefaultAsync(r => r.Id == message.CuentaBancariaId, cancellationToken);

        if (cuentaBancaria == null)
            return Result.Error($"[NO_EXISTE][{nameof(FI_PersonaCuentaBancaria)}]");

        var result = _mapper.Map<CuentaBancariaEditDto>(cuentaBancaria);
        return Result.Success(result);
    }
}