namespace Yggdrasil.Module.Credito.UI.Services.Configuacion.DTOs;

public class SeccionEditDto
{
    public int SeccionId { get; set; }
    public string NomSeccion { get; set; } = "";
    public bool Required { get; set; }
    public bool ActivoCreate { get; set; }
    public bool ActivoEdit { get; set; }
    public bool ActivoExtension { get; set; }


    public bool IsCreate { get; set; }
    public bool IsEdit { get; set; }
    public bool IsExtension { get; set; }


    public bool Obligatorio { get; set; }


}