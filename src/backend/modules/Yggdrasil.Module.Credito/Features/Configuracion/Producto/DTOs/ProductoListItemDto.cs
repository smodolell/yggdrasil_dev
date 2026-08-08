namespace Yggdrasil.Module.Credito.Features.Configuracion.Producto.DTOs;

public class ProductoListItemDto
{
    public int Id { get; set; }
    public string NomProducto { get; set; } = "";
    public string NomMoneda { get; set; } = "";
    public string NomEmpresaOtorgante { get; set; } = "";
}
