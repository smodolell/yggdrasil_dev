namespace Yggdrasil.Module.Catalog.UI.Services.Catalogos.DTOs;

public class MonedaListItemDto
{
    public int Id { get; set; }
    public string NomMoneda { get; set; } = "";
    public string ClaveMoneda { get; set; } = "";
    public bool PorDefecto { get; set; }
}
