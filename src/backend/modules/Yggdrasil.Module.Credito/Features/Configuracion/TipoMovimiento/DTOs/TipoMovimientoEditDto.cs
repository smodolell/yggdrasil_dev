namespace Yggdrasil.Module.Credito.Features.Configuracion.TipoMovimiento.DTOs;

public class TipoMovimientoEditDto
{
    public int TipoMovimientoId { get; set; }
    public string Clave { get; set; } = "";
    public string NomTipoMovimiento { get; set; } = "";
    public bool GeneraIvaCapital { get; set; }
    public bool GeneraIvaInteres { get; set; }
    public bool EsCargoInicial { get; set; }
    public bool EsConceptoFinanciado { get; set; }
    public bool Activo { get; set; }
}
