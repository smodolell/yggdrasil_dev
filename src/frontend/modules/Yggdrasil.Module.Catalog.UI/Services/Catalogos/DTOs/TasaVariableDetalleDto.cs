namespace Yggdrasil.Module.Catalog.UI.Services.Catalogos.DTOs;

public class TasaVariableDetalleDto
{
    public int Id { get; set; }
    public string NomTasa { get; set; } = "";
    public List<TasaValorListItemDto> Valores { get; set; } = new();
}
