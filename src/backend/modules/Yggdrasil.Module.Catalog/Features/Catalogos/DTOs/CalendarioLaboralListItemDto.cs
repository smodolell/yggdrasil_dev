namespace Yggdrasil.Module.Catalog.Features.Catalogos.DTOs;

public class CalendarioLaboralListItemDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public bool EsHabil { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}
