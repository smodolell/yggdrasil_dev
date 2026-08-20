namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;

public class TipoMovimientoCsListItemDto
{
    public int Id { get; set; }
    public string Clave { get; set; } = "";
    public string NomTipoMovimiento { get; set; } = "";
    public bool Activo { get; set; }
}
