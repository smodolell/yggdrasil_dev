namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;
public class PlanDocumentacionListItemDto
{
    public int DocumentacionId { get; set; }
    public string NomDocumentacion { get; set; } = "";
    public string NomTipoPersona { get; set; } = "";
    public string NomPerfil{ get; set; } = "";
    public int Orden { get; set; }

    public int PlanId { get; set; }
    public int PerfilId { get; set; }


    public bool Activo { get; set; }

}
