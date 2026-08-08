namespace Yggdrasil.Module.Credito.UI.Services.Configuacion.DTOs;

public class ProductoDetailDto
{
    public int ProductoId { get; set; }
    public string NomProducto { get; set; } = "";
    public string ClaveProducto { get; set; } = "";
    public string NomEmpresaOtorgante { get; set; } = "";
    public string NomMoneda { get; set; } = "";
    public string Posfijo { get; set; } = "";
    public string Prefijo { get; set; } = "";
    public int Consecutivo { get; set; }
    public string NomTipoMovimientoRenta { get; set; } = "";
    public string NomTipoMovimientoMora { get; set; } = "";
    public bool Activo { get; set; }
}
