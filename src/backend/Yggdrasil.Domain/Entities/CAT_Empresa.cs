namespace Yggdrasil.Domain.Entities;

public class CAT_Empresa
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string NomEmpresa { get; set; } = "";

}