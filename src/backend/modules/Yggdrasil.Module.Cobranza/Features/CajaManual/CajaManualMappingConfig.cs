using Mapster;
using Yggdrasil.Module.Cobranza.Features.CajaManual.DTOs;

namespace Yggdrasil.Module.Cobranza.Features.CajaManual;

public class CajaManualMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // FI_Movimiento → MovimientoPendienteDto (todos los campos coinciden por nombre)
        config.NewConfig<FI_Movimiento, MovimientoPendienteDto>();

        config.NewConfig<FI_Movimiento, CajaManualItemDto>()
            .Map(d => d.MovimientoId, s => s.Id);
    }
}
