namespace Yggdrasil.Module.Catalog.UI.Services.Catalogos.DTOs;

public class TasaVariableListItemDto
{
    public int Id { get; set; }
    public string NomTasa { get; set; } = string.Empty;
    public decimal? ValorTasa { get; set; }
    public DateTime? FecUltimoValor { get; set; }
}
