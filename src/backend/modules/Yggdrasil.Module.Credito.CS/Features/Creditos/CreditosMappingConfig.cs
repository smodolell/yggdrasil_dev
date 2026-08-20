using Mapster;
using Yggdrasil.Module.Credito.CS.Features.Creditos.DTOs;

namespace Yggdrasil.Module.Credito.CS.Features.Creditos;

public class CreditosMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // CS_Credito → CreditoListItemDto
        config.NewConfig<CS_Credito, CreditoCsListItemDto>()
            .Map(d => d.NomEstatusCredito, s => s.CS_EstatusCredito != null ? s.CS_EstatusCredito.NomEstatusCredito : "")
            .Map(d => d.NomTipoCredito, s => s.CS_TipoCredito != null ? s.CS_TipoCredito.NomTipoCredito : "");

        // CS_Credito → CreditoDetailDto
        config.NewConfig<CS_Credito, CreditoCsDetailDto>()
            .Map(d => d.NomEstatusCredito, s => s.CS_EstatusCredito != null ? s.CS_EstatusCredito.NomEstatusCredito : "")
            .Map(d => d.NomTipoCredito, s => s.CS_TipoCredito != null ? s.CS_TipoCredito.NomTipoCredito : "")
            .Map(d => d.NomPeriodicidad, s => s.CAT_Periodicidad != null ? s.CAT_Periodicidad.NomPeriodicidad : "")
            .Map(d => d.NomMetodoArmotizacion, s => s.CS_MetodoArmotizacion != null ? s.CS_MetodoArmotizacion.NomMetodoArmotizacion : "");

        // CS_TablaAmortiza → TablaAmortizaItemDto
        config.NewConfig<CS_TablaAmortiza, TablaAmortizaCsItemDto>()
            .Map(d => d.NomTipoMovimiento, s => s.CS_TipoMovimiento != null ? s.CS_TipoMovimiento.NomTipoMovimiento : "");
    }
}
