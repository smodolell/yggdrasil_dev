namespace Yggdrasil.Module.Catalog.UI.Services.Catalogos.DTOs;

public class BancoListItemDto
{
    public int Id { get; set; }
    public string NomBanco { get; set; } = "";
    public string CodigoBCRA { get; set; } = "";
    public string CBUPrefix { get; set; } = "";
}