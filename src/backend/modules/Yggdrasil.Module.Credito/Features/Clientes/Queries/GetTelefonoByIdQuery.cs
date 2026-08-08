using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Queries;

public record GetTelefonoByIdQuery(int TelefonoId) : IQuery<Result<TelefonoEditDto>>;

internal class GetTelefonoByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetTelefonoByIdQuery, Result<TelefonoEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<TelefonoEditDto>> HandleAsync(GetTelefonoByIdQuery message, CancellationToken cancellationToken = default)
    {
        var telefono = await _context.FI_Telefono
            .SingleOrDefaultAsync(r => r.Id == message.TelefonoId, cancellationToken);

        if (telefono == null)
            return Result.Error($"[NO_EXISTE][{nameof(FI_Telefono)}]");

        var result = _mapper.Map<TelefonoEditDto>(telefono);
        return Result.Success(result);
    }
}