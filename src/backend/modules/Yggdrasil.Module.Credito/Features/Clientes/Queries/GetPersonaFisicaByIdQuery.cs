using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Queries;

public record GetPersonaFisicaByIdQuery(int PersonaId) : IQuery<Result<PersonaFisicaEditDto>>;

internal class GetPersonaFisicaByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetPersonaFisicaByIdQuery, Result<PersonaFisicaEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<PersonaFisicaEditDto>> HandleAsync(
        GetPersonaFisicaByIdQuery message,
        CancellationToken cancellationToken = default)
    {
        var persona = await _context.FI_Persona
            .SingleOrDefaultAsync(r => r.Id == message.PersonaId, cancellationToken);

        if (persona == null)
            return Result.NotFound($"[NO_EXISTE][{nameof(FI_Persona)}]");

        var result = _mapper.Map<PersonaFisicaEditDto>(persona);
        return Result.Success(result);
    }
}