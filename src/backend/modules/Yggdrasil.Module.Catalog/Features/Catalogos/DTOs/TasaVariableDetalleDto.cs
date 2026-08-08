namespace Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

public class TasaVariableDetalleDto
{
    public int Id { get; set; }
    public string NomTasa { get; set; } = string.Empty;
    public List<TasaValorListItemDto> Valores { get; set; } = new();
}
