namespace Yggdrasil.Domain.Entities;

public class CS_EstatusCredito
{
    public int Id { get; set; }

    public string NomEstatusCredito { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;
}
