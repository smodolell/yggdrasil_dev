using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Queries;

public record GetSeccionClienteEditQuery(int PersonaId) : IQuery<Result<ClienteEditDto>>;

internal class GetSeccionClienteEditQueryHandler(IApplicationDbContext context, IMapper mapper) : IQueryHandler<GetSeccionClienteEditQuery, Result<ClienteEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<ClienteEditDto>> HandleAsync(GetSeccionClienteEditQuery message, CancellationToken cancellationToken = default)
    {
        var oPersona = await _context.FI_Persona.SingleOrDefaultAsync(r => r.Id == message.PersonaId);
        if (oPersona == null) return Result.NotFound($"[NO_EXISTE][{nameof(FI_Persona)}]");
        var model = _mapper.Map<ClienteEditDto>(oPersona);
        return Result.Success(model);
    }
}