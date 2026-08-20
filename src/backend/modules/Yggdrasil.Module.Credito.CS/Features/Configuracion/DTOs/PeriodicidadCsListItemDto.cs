namespace Yggdrasil.Module.Credito.CS.Features.Configuracion.DTOs;

public class PeriodicidadCsListItemDto
{
    public int Id { get; set; }
    public string ClavePeriodicidad { get; set; } = "";
    public string NomPeriodicidad { get; set; } = "";
    public short ParamDias { get; set; }
    public short ParamMes { get; set; }
    public short NroPagosAnio { get; set; }
    public short NroPagosMes { get; set; }
    public bool UsaDias { get; set; }
    public bool Activo { get; set; }
}
