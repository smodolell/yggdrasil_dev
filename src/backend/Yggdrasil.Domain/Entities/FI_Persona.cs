namespace Yggdrasil.Domain.Entities;

public class FI_Persona
{
    public int Id { get; set; }
    public string Identificador { get; set; } = "";
    public int PerfilId { get; set; }
    public int TipoPersonaId { get; set; } = 1;
    public int GeneroId { get; set; }
    public int EdoCivilId { get; set; }
    public string? LugarNacimientoId { get; set; }
    public DateTime FechaRegistro { get; set; }
    public string PrimerNombre { get; set; } = "";
    public string SegundoNombre { get; set; } = "";
    public string ApellidoPaterno { get; set; } = "";
    public string ApellidoMaterno { get; set; } = "";
    public string RFC { get; set; } = "";
    public string CURP { get; set; } = "";
    public string NSS { get; set; } = "";
    public DateTime? FechaNacimiento { get; set; }
    public string RazonSocial { get; set; } = "";
    public DateTime? FechaConstitucion { get; set; }



    public DateTime FechaAltaCliente { get; set; }

    public string Email { get; set; } = "";



    public CAT_TipoPersona CAT_TipoPersona { get; set; } = null!;


    public CAT_Genero CAT_Genero { get; set; } = null!;

    public CAT_EdoCivil CAT_EdoCivil { get; set; } = null!;
    public FI_Perfil FI_Perfil { get; set; } = null!;

    
    //public CAT_LugarNacimiento CAT_LugarNacimiento { get; set; } = null!;

    public ICollection<FI_Credito> FI_Credito { get; set; } = new HashSet<FI_Credito>();

    public ICollection<FI_Domicilio> FI_Domicilio { get; set; } = new HashSet<FI_Domicilio>();

    public ICollection<FI_Telefono> FI_Telefono { get; set; } = new HashSet<FI_Telefono>();
    public ICollection<FI_PersonaPerfil> FI_PersonaPerfil { get; set; } = new HashSet<FI_PersonaPerfil>();




}
