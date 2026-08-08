using Mapster;
using Yggdrasil.Module.Credito.Features.Clientes.DTOs;

namespace Yggdrasil.Module.Credito.Features.Clientes;

public class ClientesMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // FI_Persona → PersonaListItemDto
        config.NewConfig<FI_Persona, PersonaListItemDto>()
            .Map(d => d.NomPerfil, s => s.FI_Perfil.NomPerfil);





    }
}
