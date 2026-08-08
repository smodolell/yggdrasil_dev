namespace Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.Specifications;

public class TipoMovimientoSpec : Specification<FI_TipoMovimiento>
{
    public TipoMovimientoSpec(string? searchText)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p => p.Clave.Contains(searchText) || p.NomTipoMovimiento.Contains(searchText));
        }
    }

    public TipoMovimientoSpec(string? searchText, bool? activo) : this(searchText)
    {
        if (activo.HasValue)
        {
            Query.Where(p => p.Activo == activo.Value);
        }
    }
    public TipoMovimientoSpec(
        string? searchText = null,
        bool? generaIvaCapital = null,
        bool? generaIvaInteres = null,
        bool? esCargoInicial = null,
        bool? esConceptoFinanciado = null,
        bool? activo = null)
    {
        if (!string.IsNullOrEmpty(searchText))
        {
            Query.Where(p => p.Clave.Contains(searchText) || p.NomTipoMovimiento.Contains(searchText));
        }

        if (generaIvaCapital.HasValue)
        {
            Query.Where(p => p.GeneraIvaCapital == generaIvaCapital.Value);
        }

        if (generaIvaInteres.HasValue)
        {
            Query.Where(p => p.GeneraIvaInteres == generaIvaInteres.Value);
        }

        if (esCargoInicial.HasValue)
        {
            Query.Where(p => p.EsCargoInicial == esCargoInicial.Value);
        }

        if (esConceptoFinanciado.HasValue)
        {
            Query.Where(p => p.EsConceptoFinanciado == esConceptoFinanciado.Value);
        }

        if (activo.HasValue)
        {
            Query.Where(p => p.Activo == activo.Value);
        }
    }
}
