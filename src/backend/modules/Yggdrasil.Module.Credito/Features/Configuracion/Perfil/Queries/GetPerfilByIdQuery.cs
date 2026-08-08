using Yggdrasil.Module.Credito.Features.Configuracion.Perfil.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Perfil.Queries;

public record GetPerfilByIdQuery(int PerfilId) : IQuery<Result<PerfilEditDto>>;

internal class GetPerfilByIdQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IQueryHandler<GetPerfilByIdQuery, Result<PerfilEditDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<PerfilEditDto>> HandleAsync(
        GetPerfilByIdQuery message,
        CancellationToken cancellationToken = default)
    {
        var secciones = await _context.FI_Seccion.ToListAsync(cancellationToken);
        var result = new PerfilEditDto
        {
            Items = _mapper.Map<List<SeccionEditDto>>(secciones),
        };

        if (message.PerfilId == 0)
            return Result.Success(result);

        var oPerfil = await _context.FI_Perfil
            .SingleOrDefaultAsync(r => r.Id == message.PerfilId, cancellationToken);

        if (oPerfil == null)
            return Result.NotFound($"[NO_EXISTE][{nameof(FI_Perfil)}]");

        foreach (var item in result.Items)
        {
            var oPerfilSeccion = await _context.FI_PerfilSeccion
                .SingleOrDefaultAsync(r => r.SeccionId == item.SeccionId && r.PerfilId == oPerfil.Id, cancellationToken);

            if (oPerfilSeccion == null) continue;

            item.ActivoCreate = oPerfilSeccion.ActivoCreate;
            item.ActivoEdit = oPerfilSeccion.ActivoEdit;
            item.ActivoExtension = oPerfilSeccion.ActivoExtension;
        }

        _mapper.Map(oPerfil, result);

        return Result.Success(result);
    }
}