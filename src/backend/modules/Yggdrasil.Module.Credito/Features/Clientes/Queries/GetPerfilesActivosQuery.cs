using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes.Queries;

public class GetPerfilesActivosQuery : IQuery<Result<List<PerfilDto>>>
{

}
internal class GetPerfilesActivosQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetPerfilesActivosQuery, Result<List<PerfilDto>>>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result<List<PerfilDto>>> HandleAsync(
        GetPerfilesActivosQuery message,
        CancellationToken cancellationToken = default)
    {
        var result = await _context.FI_Perfil
            .Where(r => r.Activo)
            .Select(s => new PerfilDto
            {
                PerfilId = s.Id,
                NomPerfil = s.NomPerfil
            }).ToListAsync(cancellationToken);
        return Result.Success(result);
    }
}

public class GetSeccionesByPerfilIdQuery : IQuery<Result<List<SeccionPersonaDto>>>
{
    public int PerfilId { get; set; }
}

internal class GetSeccionesByPerfilIdQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetSeccionesByPerfilIdQuery, Result<List<SeccionPersonaDto>>>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result<List<SeccionPersonaDto>>> HandleAsync(
        GetSeccionesByPerfilIdQuery message,
        CancellationToken cancellationToken = default)
    {
        var secciones = await _context.FI_Seccion
            .ToListAsync(cancellationToken);
        var result = secciones.Select(s => new SeccionPersonaDto
        {
            SeccionId = s.Id,
            NomSeccion = s.NomSeccion
        }).ToList();

        if (message.PerfilId == 0)
            return Result.Success(result);
        var perfilSecciones = await _context.FI_PerfilSeccion
            .Where(r => r.PerfilId == message.PerfilId)
            .ToListAsync(cancellationToken);

        foreach (var item in result)
        {
            var oPerfilSeccion = perfilSecciones
                .SingleOrDefault(r => r.SeccionId == item.SeccionId);
            if (oPerfilSeccion == null) continue;
            item.IsCreate = oPerfilSeccion.ActivoCreate;
            item.IsEdit = oPerfilSeccion.ActivoEdit;
            item.IsExtension = oPerfilSeccion.ActivoExtension;
        }
        return Result.Success(result);
    }
}