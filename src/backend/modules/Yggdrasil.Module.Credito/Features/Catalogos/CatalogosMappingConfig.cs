using Mapster;

namespace Yggdrasil.Module.Credito.Features.Catalogos;

public class CatalogosMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.NewConfig<FI_TipoPago, TipoPagoEditDto>()
        //    .Map(d => d.TipoPagoId, s => s.Id);
    }
}
