using Yggdrasil.Module.Credito.Features.Configuracion.Perfil.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Perfil.Commands;

public record SavePerfilCommand(PerfilEditDto Model) : ICommand<Result<int>>;

internal class SavePerfilCommandHandler(
    IApplicationDbContext context,
    IMapper mapper
) : ICommandHandler<SavePerfilCommand, Result<int>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<int>> HandleAsync(
        SavePerfilCommand message,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var dto = message.Model;
            var oPerfil = await _context.FI_Perfil
                .Include(i => i.FI_PerfilSeccion)
                .SingleOrDefaultAsync(r => r.Id == dto.PerfilId, cancellationToken);

            if (oPerfil == null && dto.PerfilId != null && dto.PerfilId != 0)
                return Result.CriticalError($"[NO_EXISTE][{nameof(FI_Perfil)}]");

            if (oPerfil == null)
            {
                oPerfil = new FI_Perfil();
                _context.FI_Perfil.Add(oPerfil);
            }

            _mapper.Map(dto, oPerfil);

            foreach (var item in dto.Items)
            {
                var oPerfilSeccion = oPerfil.FI_PerfilSeccion
                    .SingleOrDefault(r => r.SeccionId == item.SeccionId);

                if (oPerfilSeccion == null)
                {
                    oPerfilSeccion = new FI_PerfilSeccion
                    {
                        SeccionId = item.SeccionId,
                    };
                    oPerfil.FI_PerfilSeccion.Add(oPerfilSeccion);
                }

                oPerfilSeccion.ActivoCreate = item.ActivoCreate;
                oPerfilSeccion.ActivoEdit = item.ActivoEdit;
                oPerfilSeccion.ActivoExtension = item.ActivoExtension;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(oPerfil.Id, "Lo datos se guardaron correctamente");
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}