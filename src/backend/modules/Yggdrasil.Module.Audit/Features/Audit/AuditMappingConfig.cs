using Mapster;
using Yggdrasil.Module.Audit.Features.Audit.DTOs;

namespace Yggdrasil.Module.Audit.Features.Audit;

public class AuditMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SYS_Audit, AuditListItemDto>()
            .Map(o => o.Description, d => d.SYS_AuditEvent.Description);
    }
}
