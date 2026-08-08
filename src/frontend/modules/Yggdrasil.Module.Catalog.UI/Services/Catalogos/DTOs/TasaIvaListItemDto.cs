namespace Yggdrasil.Module.Catalog.UI.Services.Catalogos.DTOs;

public class TasaIvaListItemDto
{
    public int Id { get; set; }
    public decimal ValorTasa { get; set; }
    public string NomTasaIva { get; set; } = "";
    public bool Activo { get; set; }
}
