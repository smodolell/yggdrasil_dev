namespace Yggdrasil.Module.Catalog.UI.Services.Catalogos.DTOs;

public class TasaListItemDto
{
    public int Id { get; set; }
    public decimal ValorTasa { get; set; }
    public string NomTasa { get; set; } = "";
    public bool Activo { get; set; }
}
