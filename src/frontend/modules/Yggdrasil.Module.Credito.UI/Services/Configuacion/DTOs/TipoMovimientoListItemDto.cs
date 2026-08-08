namespace Yggdrasil.Module.Credito.UI.Services.Configuacion.DTOs;

public class TipoMovimientoListItemDto
{
    public int Id { get; set; }
    public string Clave { get; set; } = "";
    public string NomTipoMovimiento { get; set; } = "";
    public bool EsCargoInicial { get; set; }
    public bool EsConceptoFinanciado { get; set; }
    public bool Activo { get; set; }
}
