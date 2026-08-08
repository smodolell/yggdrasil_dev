using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Queries;

public record GetPerfilByPersonaIdQuery(int PersonaId) : IQuery<Result<PerfilDto>>;

internal class GetPerfilByPersonaIdQueryHandler(IApplicationDbContext context) : IQueryHandler<GetPerfilByPersonaIdQuery, Result<PerfilDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<PerfilDto>> HandleAsync(GetPerfilByPersonaIdQuery message, CancellationToken cancellationToken = default)
    {
        var oPerfil = await _context.FI_Persona
            .Include(i => i.FI_Perfil)
            .Where(r => r.Id == message.PersonaId)
            .Select(s => s.FI_Perfil)
         .FirstOrDefaultAsync(cancellationToken);

        if (oPerfil == null) return Result.NotFound("Cliente no Encontrado");
        var result = new PerfilDto
        {
            PerfilId = oPerfil.Id,
            NomPerfil = oPerfil.NomPerfil,
        };
        return Result.Success(result);
    }
}
