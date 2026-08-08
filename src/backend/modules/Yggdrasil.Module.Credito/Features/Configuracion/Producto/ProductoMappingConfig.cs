using Mapster;
using Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;

namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto;

public class ProductoMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // FI_Producto → ProductoEditDto
        config.NewConfig<FI_Producto, ProductoEditDto>()
            .Map(d => d.ProductoId, s => s.Id)
            .Ignore(d => d.Consecutivo);

        // ProductoEditDto → FI_Producto
        config.NewConfig<ProductoEditDto, FI_Producto>()
            .Ignore(d => d.Consecutivo);

        // FI_Producto → ProductoListItemDto
        config.NewConfig<FI_Producto, ProductoListItemDto>()
            .Map(d => d.NomMoneda, s => s.CAT_Moneda != null ? s.CAT_Moneda.NomMoneda : "");

        // FI_Cargo → CargoInicialEditDto
        config.NewConfig<FI_Cargo, CargoInicialEditDto>()
            .Map(d => d.CargoId, s => s.Id);

        // FI_Cargo → CargoInicialListItemDto
        config.NewConfig<FI_Cargo, CargoInicialListItemDto>()
            .Map(d => d.NomTipoCalculo, s => s.FI_TipoCalculo != null ? s.FI_TipoCalculo.NomTipoCalculo : "")
            .Map(d => d.NomTipoMovimiento, s => s.FI_TipoMovimiento.NomTipoMovimiento)
            .Map(d => d.NomFormaPago, s => s.FI_FormaPago != null ? s.FI_FormaPago.NomFormaPago : "");

        // FI_Cargo → ConceptoFinanciadoEditDto
        config.NewConfig<FI_Cargo, ConceptoFinanciadoEditDto>()
            .Map(d => d.CargoId, s => s.Id);

        // FI_Cargo → ConceptoFinanciadoListItemDto
        config.NewConfig<FI_Cargo, ConceptoFinanciadoListItemDto>()
            .Map(d => d.NomTipoCalculo, s => s.FI_TipoCalculo != null ? s.FI_TipoCalculo.NomTipoCalculo : "")
            .Map(d => d.NomTipoMovimiento, s => s.FI_TipoMovimiento != null ? s.FI_TipoMovimiento.NomTipoMovimiento : "");




    }
}
