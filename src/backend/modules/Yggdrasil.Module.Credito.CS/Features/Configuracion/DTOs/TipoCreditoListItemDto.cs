namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;

public class TipoCreditoListItemDto
{
    public int Id { get; set; }
    public string ClaveTipoCredito { get; set; } = "";
    public string NomTipoCredito { get; set; } = "";
    public string NomTipoMovimientoRenta { get; set; } = "";
    public bool Activo { get; set; }
}
