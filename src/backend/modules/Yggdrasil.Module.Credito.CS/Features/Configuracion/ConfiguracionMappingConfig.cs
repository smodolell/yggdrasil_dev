using Mapster;
using Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Configuracion;

public class ConfiguracionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // CS_TipoCredito → TipoCreditoListItemDto
        config.NewConfig<CS_TipoCredito, TipoCreditoListItemDto>()
            .Map(d => d.NomTipoMovimientoRenta, s => s.CS_TipoMovimiento != null ? s.CS_TipoMovimiento.NomTipoMovimiento : "");

        // TipoCreditoEditDto → CS_TipoCredito: Consecutivo es un contador de negocio
        // administrado por la operación de créditos, no por este CRUD.
        config.NewConfig<TipoCreditoCsEditDto, CS_TipoCredito>()
            .Ignore(d => d.Consecutivo);
    }
}
