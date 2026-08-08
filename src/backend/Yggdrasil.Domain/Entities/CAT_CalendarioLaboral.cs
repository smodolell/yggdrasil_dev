namespace Yggdrasil.Domain.Entities;

public class CAT_CalendarioLaboral
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public bool EsHabil { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}
