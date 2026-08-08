using Mapster;
using Yggdrasil.Module.Credito.Features.Configuracion.Perfil.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Perfil;

public class PerfilMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        

        // FI_Perfil → PerfilEditDto
        config.NewConfig<FI_Perfil, PerfilEditDto>()

            .Map(d => d.PerfilId, s => s.Id);

        // FI_Seccion → SeccionEditDto
        config.NewConfig<FI_Seccion, SeccionEditDto>()
            .Map(d => d.SeccionId, s => s.Id);



    }
}
